import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { BaseComponent } from '../../common/base.component';
import { Article } from '../../common/model/article.model';
import { VAT } from '../../common/model/vat.model';
import { ArticleService } from '../../common/service/article.service';

@Component({
  selector: 'app-add-article',
  templateUrl: './add-article.component.html',
  styleUrls: ['./add-article.component.scss']
})
export class AddArticleComponent extends BaseComponent implements OnInit {

  @Input() article!: Article;
  articleForm!: FormGroup;
  hideVATList: boolean = true;

  constructor(
    private _fb: FormBuilder,
    private _articleService: ArticleService,
    private _dialog: MatDialog,
    private _snackbar: MatSnackBar,
    private _router: Router
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
            Validators.required
          ]
        ],
        unitpriceincludingvat: [
          0,
          [
            Validators.required
          ]
        ],
        vat: formBuilder.group({
          id: [{ value:0, disabled: true}],
          code: [{value:'', disabled: true}],
          percentage: [{value:0, disabled: true}],
          description: [{value:'', disabled: true}]
        })
      })
    });
  }

  ngOnInit(): void {
    this.hideVATList = true;
    this.subscribeToErrors<Article>(this._articleService);

    this.article = new Article();
    this.articleForm = this.CreateFormGroup(this._fb);
  }

  onSubmit() {
    if (this.articleForm?.valid) {
      this.article.updatePartial(this.articleForm.get('article')?.value);
      this.addArticle();
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


  addArticle() {
    this._articleService.createArticle$(this.article).subscribe(response => {
      if (response.id != null && response.id > 0) {
        this._router.navigate(['/article']);
        this._snackbar.open('Article has been created.', 'ok', { duration: 2000 });
      }
    });
  }


}
