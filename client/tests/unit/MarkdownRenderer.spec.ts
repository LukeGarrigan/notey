import { shallowMount } from '@vue/test-utils';
import MarkdownRenderer from '@/components/MarkdownRenderer.vue';

describe('MarkdownRenderer.vue', () => {

  it('should render as is without any modifications because no md syntax', () => {
    const value = 'No special syntax';
    const wrapper = shallowMount(MarkdownRenderer, {
      propsData: { value },
    });
    expect((wrapper.vm as any).convertToHtml()).toMatch(value);
  });

  it('Should wrap in h1 tags', () => {
    const value = '# I should be a title';
    const wrapper = shallowMount(MarkdownRenderer, {
      propsData: { value },
    });
    expect((wrapper.vm as any).convertToHtml()).toMatch(`<h1 id="ishouldbeatitle">I should be a title</h1>`);
  });

  it('Should wrap in h2 tags', () => {
    const value = '## I should be a title';
    const wrapper = shallowMount(MarkdownRenderer, {
      propsData: { value },
    });
    expect((wrapper.vm as any).convertToHtml()).toMatch(`<h2 id="ishouldbeatitle">I should be a title</h2>`);
  });

  it('should create link', () => {
    const value = '[codecanvases.com](https://codecanvases.com) I should be a link';
    const wrapper = shallowMount(MarkdownRenderer, {
      propsData: { value },
    });
    expect((wrapper.vm as any).convertToHtml()).toMatch(`<p><a href="https://codecanvases.com">codecanvases.com</a> I should be a link</p>`);
  });
});
