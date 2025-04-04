document.addEventListener('DOMContentLoaded', function () {
    const searchInput = document.getElementById('search-input');
    const suggestionsBox = document.getElementById('suggestions-box');

    searchInput.addEventListener('input', function () {
        const searchTerm = this.value.trim();

        if (searchTerm.length > 0) {
            fetch(`/Product/SearchSuggestions?searchTerm=${encodeURIComponent(searchTerm)}`)
                .then(response => response.json())
                .then(data => {

                    console.log(data); // Kiểm tra dữ liệu trả về trong Console

                    suggestionsBox.innerHTML = '';

                    if (data.length > 0) {
                        data.forEach(item => {
                            const suggestionItem = document.createElement('div');
                            suggestionItem.classList.add('suggestion-item');
                            suggestionItem.textContent = item.productName;
                            suggestionItem.dataset.productId = item.id;

                            suggestionItem.addEventListener('click', function () {
                                window.location.href = `/Product/ProductDetail/${item.id}`;
                            });

                            suggestionsBox.appendChild(suggestionItem);
                        });
                        suggestionsBox.style.display = 'block';
                    } else {
                        suggestionsBox.style.display = 'none';
                    }
                })
                .catch(error => {
                    console.error('Lỗi khi gọi API:', error);
                });
        } else {
            suggestionsBox.style.display = 'none';
        }
    });

    document.addEventListener('click', function (event) {
        if (!searchInput.contains(event.target) && !suggestionsBox.contains(event.target)) {
            suggestionsBox.style.display = 'none';
        }
    });
});