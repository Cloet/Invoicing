import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticleDisplayScreenComponent } from './article-display-screen.component';

describe('ArticleDisplayScreenComponent', () => {
  let component: ArticleDisplayScreenComponent;
  let fixture: ComponentFixture<ArticleDisplayScreenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ArticleDisplayScreenComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ArticleDisplayScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
