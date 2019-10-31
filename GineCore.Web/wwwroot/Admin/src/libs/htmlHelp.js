const getTagHtml = (val, type) => {
  return `<div class="ivu-tag ivu-tag-${type} ivu-tag-closable ivu-tag-checked">
            <span class="ivu-tag-text ivu-tag-color-gray">${val}</span>
          </div>`;
} 

const getRedTagHtml = (val) => {
  return `<div class="ivu-tag ivu-tag-red ivu-tag-closable ivu-tag-checked">
            <span class="ivu-tag-text ivu-tag-color-white">${val}</span>
          </div>`;
} 

const getBlueTagHtml = (val) => {
  return `<div class="ivu-tag ivu-tag-blue ivu-tag-closable ivu-tag-checked">
            <span class="ivu-tag-text ivu-tag-color-white">${val}</span>
          </div>`;
}

const getIconHtml = (val) => {
  return `<i class="ivu-icon ivu-icon-${val}"></i>`;
}

const htmlHelper = {
  getTagHtml,
  getIconHtml,
  getRedTagHtml,
  getBlueTagHtml
}

export default htmlHelper;