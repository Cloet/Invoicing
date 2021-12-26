import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArticleDisplayScreenComponent } from './article-display-screen/article-display-screen.component';
import { ArticleTableComponent } from './article-table/article-table.component';
import { ArticleInfoComponent } from './article-info/article-info.component';
import { AddArticleComponent } from './add-article/add-article.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { ArticleService } from '../common/service/article-service';
import { VATService } from '../common/service/vat.service';

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
        path: 'add-article',
        component: AddArticleComponent,
        data: {
          breadcrumb: 'Create'
        }
      },
      {
        path: 'article-info/:id',
        component: ArticleInfoComponent,
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
    ArticleInfoComponent,
    AddArticleComponent
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
