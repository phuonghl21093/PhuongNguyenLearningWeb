web365.main = (function () {

    function initValidate() {

        $("#login").validate({
            rules: {
                username: {
                    required: true,
                    minlength: 6
                },
                password: {
                    required: true,
                    minlength: 6
                },
            },
            messages: {
                username: 'Bạn vui lòng nhập tên đăng nhập',
                password: 'Bạn vui lòng nhập mật khẩu'
            },
            submitHandler: function (e) {

                if (!$('#login').hasClass('isPosting')) {

                    $('#login').addClass('isPosting');
                    $('#login button[type="submit"]').text('đang đăng nhập...');

                    $.post('/Ajax/Customer/Login', $('#login').serialize(), function (res) {

                        if (!res.error) {
                            document.location.reload();
                        }
                        else {
                            web365.utility.toastWarning(res.message);
                        }

                    }).complete(function () {
                        $('#login').removeClass('isPosting');
                        $('#login button[type="submit"]').text('Đăng nhập');
                    });

                }

                return false;
            }
        });

	    $("#register").validate({
	        rules: {
	            name: {
	                required: true,
	                minlength: 6
	            },
	            password: {
	                required: true,
	                minlength: 6
	            },
	            repassword: {
	                equalTo: "#register-password"
	            },
	            email: {
	                required: true,
	                email: true
	            },
	            phone: {
	                required: true,
	                number: true,
	                minlength: 9
	            }
	        },
	        messages: {
	            name: 'Bạn vui lòng nhập tên',
	            email: 'Bạn vui lòng nhập email',
	            password: 'Bạn vui lòng nhập mật khẩu',
	            repassword: 'Mật khẩu không giống nhau',
	            phone: 'Bạn vui lòng nhập số điện thoại'
	        },
	        submitHandler: function (e) {

	            if (!$('#register').hasClass('isPosting')) {

	                $('#register').addClass('isPosting');

	                $('#register button[type="submit"]').text('đang đăng ký...');

	                $.post('/Ajax/Customer/Register', $('#register').serialize(), function (res) {
	                    if (!res.error) {
	                        document.location.reload();
	                    }
	                    else {
	                        web365.utility.toastWarning(res.message);
	                    }
	                }).complete(function () {
	                    $('#register').removeClass('isPosting');
	                    $('#register button[type="submit"]').text('Đăng ký');
	                });

	            }

	            return false;
	        }
	    });

	    $("#forget_password").validate({
	        rules: {
	            email: {
	                required: true,
	                email: true
	            }
	        },
	        messages: {
	            email: 'Bạn vui lòng nhập email'
	        },
	        submitHandler: function (e) {

	            if (!$('#forget_password').hasClass('isPosting')) {

	                $('#forget_password').addClass('isPosting');
	                $('#forget_password button[type="submit"]').text('đang lấy lại mật khẩu...');

	                $.post('/Ajax/Customer/ForgetPassword', $('#forget_password').serialize(), function (res) {

	                    if (!res.error) {
	                        $('#modal-forget').modal('hide');
	                        web365.utility.toastSuccess(res.message);
	                    }
	                    else {
	                        web365.utility.toastWarning(res.message);
	                    }

	                }).complete(function () {
	                    $('#forget_password').removeClass('isPosting');
	                    $('#forget_password button[type="submit"]').text('Lấy lại mật khẩu');
	                });

	            }

	            return false;
	        }
	    });

	    $("#receive-email-form").validate({
	        rules: {
	            email: {
	                required: true,
	                email: true
	            }
	        },
	        messages: {
	            email: 'Bạn vui lòng nhập email'
	        },
	        submitHandler: function (e) {

	            if (!$('#receive-email-form').hasClass('isPosting')) {

	                $('#receive-email-form').addClass('isPosting');
	                $('#receive-email-form button[type="submit"]').text('đang gửi thông tin...');

	                $.post('/Ajax/Home/AddEmail', $('#receive-email-form').serialize(), function (res) {

	                    if (!res.error) {
	                        $('#modal-email').modal('hide');
	                        web365.utility.toastSuccess(res.message);
	                    }
	                    else {
	                        web365.utility.toastWarning(res.message);
	                    }

	                }).complete(function () {
	                    $('#receive-email-form').removeClass('isPosting');
	                    $('#receive-email-form button[type="submit"]').text('Đăng ký nhận thông tin');
	                });

	            }

	            return false;
	        }
	    });
    }

    function initEvent() {
        $('.alogout').click(function (e) {
            e.preventDefault();

            $.post('/Ajax/Customer/Logout', {}, function (res) {
                if (!res.error) {
                    document.location.href = '/';
                }
                else {
                    web365.utility.toastWarning(res.message);
                }
            });

        });

        $('.btn-forget').click(function (e) {
            e.preventDefault();

            $('#modal-login').modal('hide');
            $('#modal-forget').modal('show');

        });
    }

	return {
	    initValidate: initValidate,
	    initEvent: initEvent
	};

})();