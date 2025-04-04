document.addEventListener('DOMContentLoaded', function () {
    const sidebar = document.getElementById('sidebar');
    const content = document.getElementById('content');
    const toggleBtn = document.getElementById('toggle-btn');
    const settingsBtn = document.querySelector('.settings-btn');
    const submenu = document.getElementById('settings-submenu');
    const menuLinks = document.querySelectorAll('.sidebar a:not(.settings-btn)');

    // Toggle sidebar
    toggleBtn.addEventListener('click', function () {
        sidebar.classList.toggle('expanded');
        content.classList.toggle('expanded');

        // Save state to localStorage
        const expanded = sidebar.classList.contains('expanded');
        localStorage.setItem('sidebarExpanded', expanded);
    });

    // Toggle submenu
    settingsBtn.addEventListener('click', function (e) {
        e.preventDefault();
        submenu.classList.toggle('active');
    });

    // Set active link
    menuLinks.forEach(link => {
        link.addEventListener('click', function () {
            menuLinks.forEach(item => item.classList.remove('active-link'));
            settingsBtn.classList.remove('active-link');
            this.classList.add('active-link');
        });
    });


    //Check localStorage for sidebar state
    //const expanded = localStorage.getItem('sidebarExpanded') === 'true';
    //if (expanded) {
    //    sidebar.classList.add('expanded');
    //    content.classList.add('expanded');
    //}
    // Add smooth animation for hover effect
    const sidebarLinks = document.querySelectorAll('.sidebar a');
    sidebarLinks.forEach(link => {
        link.addEventListener('mouseenter', function () {
            this.style.transition = 'all 0.3s ease';
        });

        link.addEventListener('mouseleave', function () {
            this.style.transition = 'all 0.3s ease';
        });
    });
    
    tinymce.init({
        selector: 'textarea',
                
        plugins: [
            // Core editing features
            'anchor', 'autolink', 'charmap', 'codesample', 'emoticons', 'image', 'link', 'lists', 'media', 'searchreplace', 'table', 'visualblocks', 'wordcount',
            // Your account includes a free trial of TinyMCE premium features
            // Try the most popular premium features until Apr 5, 2025:
            'checklist', 'mediaembed', 'casechange', 'export', 'formatpainter', 'pageembed', 'a11ychecker', 'tinymcespellchecker', 'permanentpen', 'powerpaste', 'advtable', 'advcode', 'editimage', 'advtemplate', 'ai', 'mentions', 'tinycomments', 'tableofcontents', 'footnotes', 'mergetags', 'autocorrect', 'typography', 'inlinecss', 'markdown', 'importword', 'exportword', 'exportpdf'
        ],
        toolbar: 'undo redo | blocks fontfamily fontsize | bold italic underline strikethrough | link image media table mergetags | addcomment showcomments | spellcheckdialog a11ycheck typography | align lineheight | checklist numlist bullist indent outdent | emoticons charmap | removeformat',
        tinycomments_mode: 'embedded',
        tinycomments_author: 'Author name',
        mergetags_list: [
            { value: 'First.Name', title: 'First Name' },
            { value: 'Email', title: 'Email' },
        ],
        ai_request: (request, respondWith) => respondWith.string(() => Promise.reject('See docs to implement AI Assistant')),

    });

});
