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
});
