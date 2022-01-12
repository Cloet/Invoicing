import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseComponent } from '../../common/base.component';
import { Article } from '../../common/model/article.model';
import { VAT } from '../../common/model/vat.model';
import { ArticleService } from '../../common/service/article.service';
import { DeleteArticleDialogComponent } from '../delete-article-dialog/delete-article-dialog.component';

@Component({
  selector: 'app-add-edit-article',
  templateUrl: './add-edit-article.component.html',
  styleUrls: ['./add-edit-article.component.scss']
})
export class AddEditArticleComponent extends BaseComponent implements OnInit {

  @Input() article!: Article;
  articleForm!: FormGroup;
  hideVATList: boolean = true;
  isAddMode!: boolean;
  id!: string;

  constructor(
    private _fb: FormBuilder,
    private _articleService: ArticleService,
    private _dialog: MatDialog,
    private _snackbar: MatSnackBar,
    private _router: Router,
    private _route: ActivatedRoute
  ) {
    super(_dialog);
  }

  private CreateFormGroup(formBuilder: FormBuilder): FormGroup {
    return formBuilder.group({
      article: formBuilder.group({
        articlecode: [
          '',
          [
            Validators.required,
            Validators.minLength(3)
          ]
        ],
        description: [
          '',
          [
            Validators.required,
            Validators.minLength(3)
          ]
        ],
        unitprice: [
          0,
          [
            Validators.required,
            Validators.min(0)
          ]
        ],
        unitpriceincludingvat: [
          {
            value: 0,
            disabled: true
          },
          [
            Validators.required
          ]
        ],
        vat: formBuilder.group({
          id: [{ value: 0, disabled: true }],
          code: [{ value: '', disabled: true }],
          percentage: [{ value: 0, disabled: true }],
          description: [{ value: '', disabled: true }]
        })
      })
    });
  }

  ngOnInit(): void {
    this.id = this._route.snapshot.params['id'];
    this.isAddMode = !this.id

    this.hideVATList = true;
    this.subscribeToErrors<Article>(this._articleService);

    this.article = new Article();
    this.articleForm = this.CreateFormGroup(this._fb);

    this.articleForm.get('article.unitprice')?.valueChanges.subscribe(value => {
      this.updateUnitPriceIncludingVAT();
    });
    this.articleForm.get('article.vat.percentage')?.valueChanges.subscribe(value => {
      this.updateUnitPriceIncludingVAT();
    });

    if (!this.isAddMode) {
      this._articleService.getArticleForId$(Number(this.id)).subscribe(val => {
        this.article = val;
        this.articleForm.get('article')?.patchValue(val);
      });
    }

  }

  updateUnitPriceIncludingVAT() {
    const unitprice = this.articleForm.get('article.unitprice')?.value;
    const vat = this.articleForm.get('article.vat.percentage')?.value

    this.articleForm.get('article.unitpriceincludingvat')?.patchValue(unitprice * (1 + vat / 100));
  }

  onSubmit() {
    if (this.articleForm?.valid) {
      this.article.updatePartial(this.articleForm.getRawValue().article);
      if (this.isAddMode) {
        this.addArticle();
      } else {
        this.updateArticle();
      }
    };
  }

  onSelect() {
    this.hideVATList = false;
  }

  setVAT(vat: VAT) {
    if (vat != undefined) {
      this.article.vat = vat;
      this.articleForm.get('article.vat')?.patchValue(vat);
    }
    this.hideVATList = true;
  }

  deleteArticle() {
    this._articleService.getArticleForId$(this.article.id).subscribe(
      val => {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.autoFocus = true;
        const dialogRef = this._dialog.open(DeleteArticleDialogComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(result => {
          if (result) {
            this._articleService.deleteArticle(val.id).subscribe(() => {
              this._snackbar.open("Article has been deleted.", 'ok', { duration: 2000 });
            });
          }
        });
      }
    )
  }

  updateArticle() {
    this._articleService.updateArticle$(this.article).subscribe(response => {
      if (response.id != null && response.id > 0) {
        this._snackbar.open('Article has been saved.', 'ok', { duration: 2000 });
      }
    })
  }

  addArticle() {
    this._articleService.createArticle$(this.article).subscribe(response => {
      if (response.id != null && response.id > 0) {
        this._router.navigate(['/article']);
        this._snackbar.open('Article has been created.', 'ok', { duration: 2000 });
      }
    });
  }

}
