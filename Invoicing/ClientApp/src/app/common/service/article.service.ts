import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, of } from "rxjs";
import { catchError, map, tap } from "rxjs/operators";
import { Article } from "../model/article.model";
import { BaseService } from "./base.service";

@Injectable({
  providedIn: 'root'
})
export class ArticleService extends BaseService<Article> {

  public _articles = new BehaviorSubject<Article[]>([]);

  private getArticlesArray$(): Observable<Article[]> {
    return this._articles.asObservable();
  }

  public getArticleForId$(articleId: number): Observable<Article> {
    const headers = this.defaultHeaders();
    const options = { headers };

    return this.http.get<Article>(`${this.serviceUrl}/article/${articleId}`, options)
      .pipe(
        catchError(
          error => {
            this.loadingError$.next(this.getErrorMsg(error));
            this.handleError(error);
            return of(error);
          }
        ),
        map(Article.fromJson)
      );
  }

  public getArticles$(): Observable<Article[]> {
    const headers = this.defaultHeaders();
    const options = { headers };

    this.http.get(`${this.serviceUrl}/article/`, options)
      .pipe(
        catchError(
          error => {
            this.loadingError$.next(this.getErrorMsg(error));
            this.handleError(error);
            return of(error);
          }
        ),
        map((list: any[]): Article[] => list.map(Article.fromJson))
      ).subscribe(res => this._articles.next(res));

    return this.getArticlesArray$();
  }

  public createArticle$(article: Article): Observable<Article> {
    const headers = this.defaultHeaders();
    const options = { headers };
    const payload = JSON.stringify(article);

    return this.http.post<Article>(`${this.serviceUrl}/article/`, payload, options)
      .pipe(
        catchError(error => {
          this.postError$.next(this.getErrorMsg(error));
          this.handleError(error);
          return of(error);
        }),
        tap(x => this.addItemToArray(Article.fromJson(x), this._articles)),
        map(Article.fromJson)
      );
  }

  public updateArticle$(article: Article): Observable<Article> {
    const headers = this.defaultHeaders();
    const options = { headers };
    const payload = JSON.stringify(article);

    return this.http.put(`${this.serviceUrl}/article/${article.id}`, payload, options)
      .pipe(
        catchError(error => {
          this.putError$.next(this.getErrorMsg(error));
          this.handleError(error);
          return of(error);
        }),
        tap(x => this.updateItemInArray(Article.fromJson(x), this._articles)),
        map(Article.fromJson)
      );
  }

  deleteArticle(id: number) {
    const headers = this.defaultHeaders();
    const options = { headers };

    return this.http.delete(`${this.serviceUrl}/article/${id}`, options)
      .pipe(
        catchError(error => {
          this.deleteError$.next(this.getErrorMsg(error));
          this.handleError(error);
          return of(error);
        }),
        tap((res => this.removeFromArray(id, this._articles)))
      );
  }




}
