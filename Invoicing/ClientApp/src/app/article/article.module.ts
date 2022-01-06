import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArticleDisplayScreenComponent } from './article-display-screen/article-display-screen.component';
import { ArticleTableComponent } from './article-table/article-table.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { VATService } from '../common/service/vat.service';
import { DeleteArticleDialogComponent } from './delete-article-dialog/delete-article-dialog.component';
import { ArticleService } from '../common/service/article.service';
import { VatSelectionComponent } from '../shared/vat-selection/vat-selection.component';
import { AddEditArticleComponent } from './add-edit-article/add-edit-article.component';

const articleRoutes: Routes = [
  {
    path: '',
    component: ArticleDisplayScreenComponent,
    data: {
      breadcrumb: 'Article'
    },
    children: [
      {
        path: '',
        component: ArticleTableComponent
      },
      {
        path: 'add',
        component: AddEditArticleComponent,
        data: {
          breadcrumb: 'Create'
        }
      },
      {
        path: 'edit/:id',
        component: AddEditArticleComponent,
        data: {
          breadcrumb: 'Info'
        }
      }
    ]
  }
]


@NgModule({
  declarations: [
    ArticleDisplayScreenComponent,
    ArticleTableComponent,
    DeleteArticleDialogComponent,
    AddEditArticleComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(articleRoutes)
  ],
  providers: [
    ArticleService,
    VATService
  ]
})
export class ArticleModule { }
