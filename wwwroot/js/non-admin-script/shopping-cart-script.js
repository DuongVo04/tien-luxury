document.addEventListener('DOMContentLoaded', function () {
    const paymentOptions = document.querySelectorAll('.payment-option');
    const paymentMethodInput = document.getElementById('payment-method');

    paymentOptions.forEach(option => {
        option.addEventListener('click', function () {
            const method = this.dataset.method;

            if (method === 'bank' || method === 'momo') {
                alert(`Tính năng thanh toán ${method === 'bank' ? 'chuyển khoản ngân hàng' : 'Momo'} đang được phát triển`);
                return;
            }

            paymentOptions.forEach(opt => opt.classList.remove('active'));
            this.classList.add('active');
            paymentMethodInput.value = method;
        });
    });


    document.querySelector('.checkout-btn').addEventListener('click', function (e) {
        if (!paymentMethodInput.value) {
            e.preventDefault();
            alert('Vui lòng chọn phương thức thanh toán');
        }
    });

    const updateTotal = () => {
        let total = 0;
        document.querySelectorAll('.cart-item').forEach(item => {
            const price = parseInt(item.querySelector('.cart-item-price').textContent.replace(/[^0-9]/g, ''));
            const quantity = parseInt(item.querySelector('.quantity-input').value);

            total += price * quantity;
        });
        document.getElementById('total-payment').textContent = total.toLocaleString('vi-VN') + ' ₫';
    };

    document.querySelectorAll('.quantity-btn').forEach(button => {
        button.addEventListener('click', function () {
            const input = this.parentElement.querySelector('.quantity-input');

            let value = parseInt(input.value);

            if (this.classList.contains('increase')) {
                value++;
            } else if (this.classList.contains('decrease') && value > 1) {
                value--;
            }
            input.value = value;
            updateTotal();
        });
    });

    document.querySelectorAll('.remove-item').forEach(button => {
        button.addEventListener('click', function () {
            this.closest('.cart-item').remove();
            updateTotal();
        });
    });

    updateTotal();

    let provincesData = [];
    let districtData = [];
    const provinceSelect = $("#province-select");
    const districtSelect = $("#district-select");
    const wardSelect = $("#ward-select");

    $.ajax({
        url: 'https://provinces.open-api.vn/api/?depth=3',
        method: 'GET',
        dataType: 'json',
        success: function (data) {

            provincesData = data;

            provinceSelect.empty().append('<option value="">---</option>');


            data.forEach(province => {
                const option = `<option value="${province.code}">${province.name}</option>`;
                provinceSelect.append(option);
            });
        },
    });


    provinceSelect.change(function () {

        const provinceCode = $(this).val();


        districtSelect.empty().append('<option value="">---</option>');
        wardSelect.empty().append('<option value="">---</option>');

        if (provinceCode) {
            const selectedProvince = provincesData.find(p => p.code == provinceCode);

            console.log(selectedProvince);
            districtData = selectedProvince.districts;

            districtData.forEach(district => {
                const option = `<option value="${district.code}">${district.name}</option>`;
                districtSelect.append(option);
            });
        }
    });

    districtSelect.change(function () {

        const districtCode = $(this).val();


        wardSelect.empty().append('<option value="">---</option>');

        if (districtCode) {
            const selectedDistrict = districtData.find(p => p.code == districtCode);

            console.log(selectedDistrict);

            selectedDistrict.wards.forEach(ward => {
                const option = `<option value="${ward.code}">${ward.name}</option>`;
                wardSelect.append(option);
            });
        }
    });


    function updateFullAddress() {
        const province = provinceSelect.find('option:selected').text();
        const district = districtSelect.find('option:selected').text();
        const ward = wardSelect.find('option:selected').text();
        const specificLocation = $("#input-address").val().trim();
    
        if (province !== "---" && district !== "---" && ward !== "---" && specificLocation) {
            const fullAddress = `${specificLocation}, ${ward}, ${district}, ${province}`;
            // console.log("Địa chỉ đầy đủ:", fullAddress);
            $("#full-address").val(fullAddress);
            console.log(fullAddress);
        }
    }
    
    provinceSelect.change(updateFullAddress);
    districtSelect.change(updateFullAddress);
    wardSelect.change(updateFullAddress);
    $("#input-address").on('input', updateFullAddress);
    
});