/**
 * @license Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here.
    // For the complete reference:
    // http://docs.ckeditor.com/#!/api/CKEDITOR.config
    config.language = 'en-gb';
    config.uiColor = '#F1F1F1';

    // see http://docs.cksource.com/CKEditor_3.x/Developers_Guide/Toolbar

    config.toolbar_SlickCMS =
    [
        // '-' puts a vertical separator between icons
        { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', 'RemoveFormat'] },
        { name: 'styles', items: ['Styles', 'Format'] },
        { name: 'paragraph', items: ['NumberedList', 'BulletedList', 'Outdent', 'Indent', 'Blockquote', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'] },
        { name: 'colors', items: ['TextColor', 'BGColor'] },
        '/',
        { name: 'links', items: ['Link', 'Unlink'] },
        { name: 'document', items: ['Source', 'Preview', 'Print'] },
        { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', 'Undo', 'Redo'] },
        { name: 'editing', items: ['Find', 'Replace', 'SelectAll'] },
        { name: 'tools', items: ['Maximize', 'ShowBlocks'] },
        { name: 'insert', items: ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak'] },
        { name: 'about' }
    ];

    config.toolbar_SlickCMSMobile =
    [
        // '-' puts a vertical separator between icons
        { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', 'RemoveFormat'] },
        '/',
        { name: 'styles', items: ['Styles', 'Format'] },
        '/',
        { name: 'paragraph', items: ['NumberedList', 'BulletedList', 'Outdent', 'Indent', 'Blockquote', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'] },
        { name: 'colors', items: ['TextColor', 'BGColor'] },
        '/',
        { name: 'links', items: ['Link', 'Unlink'] },
        { name: 'document', items: ['Source', 'Preview', 'Print'] },
        '/',
        { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', 'Undo', 'Redo'] },
        '/',
        { name: 'editing', items: ['Find', 'Replace', 'SelectAll'] },
        { name: 'tools', items: ['Maximize', 'ShowBlocks'] },
        { name: 'insert', items: ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak'] },
        { name: 'about' }
    ];

    // use a different toolbar if on a mobile/narrow device
    var bodyWidth = $('body').width();
    if (bodyWidth <= 850) {
        config.toolbar = 'SlickCMSMobile';
    } else {
        config.toolbar = 'SlickCMS';
    }
};