$(window).on("load", function() {
    "use strict";



    $(".features-dv form ul li input:checkbox").on("click", function() { return false; });

    $(".rtl-select").on("click", function() {
        window.location.href="17_Features_Example_Alt_Titlebar.rtl.html"
    });
    $(".eng-select").on("click", function() {
        window.location.href="17_Features_Example_Alt_Titlebar.html"
    });

    /*==============================================
                      Custom Dropdown
    ===============================================*/

    $('.drop-menu').on('click', function () {
        $(this).attr('tabindex', 1).focus();
        $(this).toggleClass('active');
        $(this).find('.dropeddown').slideToggle(300);
    });
    $('.drop-menu').on("focusout", function () {
        $(this).removeAttr('tabindex', 1).focus();
        $(this).removeClass('active');
        $(this).find('.dropeddown').slideUp(300);
    });
    $('.drop-menu .dropeddown li').on('click', function () {
        $(this).parents('.drop-menu').find('span').text($(this).text());
        $(this).parents('.drop-menu').find('span').addClass("selected");
        $(this).parents('.drop-menu').find('input').attr('value', $(this).attr('id'));
    });


    /*==============================================
                      POPUP FUNCTIONS
    ===============================================*/

    $(".signin-op").on("click", function() {
        $("#sign-popup").toggleClass("active");
        $("#register-popup").removeClass("active");
        $(".wrapper").addClass("overlay-bgg");
    });
    $("html").on("click", function(){
        $("#sign-popup").removeClass("active");
        $(".wrapper").removeClass("overlay-bgg");
    });
    $(".signin-op, .popup").on("click", function(e) {
        e.stopPropagation();
    });

    $(".reg-op").on("click", function() {
        $("#register-popup").toggleClass("active");
        $(".wrapper").addClass("overlay-bgg");
        $("#sign-popup").removeClass("active");
    });
    $("html").on("click", function(){
        $("#register-popup").removeClass("active");
        $(".wrapper").removeClass("overlay-bgg");
    });
    $(".reg-op, .popup").on("click", function(e) {
        e.stopPropagation();
    });

    //Open Forgot Password Modal
    $(".forgotpassword-op").on("click", function () {
        $("#forgot-password-popup").toggleClass("active");
        $("#sign-popup").removeClass("active");
        $(".wrapper").addClass("overlay-bgg");

    });

    // Close Popups on Clicking Outside
    $("html").on("click", function () {
        $(".popup").removeClass("active");
        $(".wrapper").removeClass("overlay-bgg");
    });
    $(".forgotpassword-op,.popup").on("click", function (e) {
        e.stopPropagation();
    });

    // Open Reset Password Modal after entering phone number
    $("#send-code").on("click", function () {
        $("#forgot-password-popup").removeClass("active");
        $("#reset-password-popup").toggleClass("active");
    });

    // Handle Reset Password
    $("#reset-password").on("click", function () {
        var verificationCode = $("input[name='verification-code']").val();
        var newPassword = $("input[name='new-password']").val();
        var confirmPassword = $("input[name='confirm-password']").val();

        if (newPassword === confirmPassword) {
            // Perform password reset action here (e.g., send to server)
            alert("Password has been reset successfully.");
            $("#reset-password-popup").removeClass("active");
            $(".wrapper").removeClass("overlay-bgg");
        } else {
            alert("Passwords do not match.");
        }
    });

    // edit user info
    $("#edit-op").on("click", function () {
        $("#editUserInfo-popup").toggleClass("active");
        $(".wrapper").addClass("overlay-bgg");
    });
    $("html").on("click", function () {
        $("#editUserInfo-popup").removeClass("active");
        $(".wrapper").removeClass("overlay-bgg");
    });
    $("#edit-op, .popup").on("click", function (e) {
        e.stopPropagation();
    });

    

    /*==============================================
                FEATURES TOGGLE FUNCTION
    ===============================================*/


    $(".more-feat > h3").on("click", function(){
        $(".features_list").slideToggle();
    });


    /*==============================================
                    HALF MAP POSITIONING
    ===============================================*/


    var hd_height = $("header").innerHeight();
    $(".half-map-sec #map-container.fullwidth-home-map").css({
        "top": hd_height
    });
    $(".half-map-sec").css({
        "margin-top": hd_height
    });



    /*==============================================
        SETTING POSITION ACRD TO CONTAINER
    ===============================================*/

    
    var offy = $(".container").offset().left;
    $(".banner_text").css({
      "left": offy
    });

    $(".banner_text.fr").css({
      "right": offy
    });

    
    if ($(window).width() > 768) {
        var aprt_img = $(".apartment-sec .card_bod_full").innerHeight();
        $(".apartment-sec .img-block").css({
            "height": aprt_img
        });
    };

    $(".close-menu").on("click", function(){
        $(".navbar-collapse").removeClass("show");
        return false;
    });

    
    

    /*==============================================
                      SETTING HEIGHT OF DIVS
    ===============================================*/

    var ab_height = $(".agent-info").outerHeight();
    $(".agent-img").css({
        "height": ab_height
    });


    /*==============================================
                    SMOOTH SCROLLING
    ===============================================*/

    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault();

        document.querySelector(this.getAttribute('href')).scrollIntoView({
                behavior: 'smooth'
            });
        });
    });

    /*==============================================
                      DROPDOWN EFFECT
    ===============================================*/


    $('.dropdown').on('show.bs.dropdown', function(e){
      $(this).find('.dropdown-menu').first().stop(true, true).slideDown(300);
    });

    $('.dropdown').on('hide.bs.dropdown', function(e){
      $(this).find('.dropdown-menu').first().stop(true, true).slideUp(200);
    });


    /*==============================================
                      ALERT FUNCTIONS
    ===============================================*/



    $(".popular-listing .card .card-footer a .la-heart-o").on("click", function(){
        $(".alert-success").addClass("active");
        return false;
    });
    $(".popular-listing .card .card-footer a .la-heart-o, .alert-success").on("click", function(e){
        e.stopPropagation();
    });

    $(".close-alert").on("click", function(){
        $(".alert-success").removeClass("active");
        return false;
    });
    

    /*==============================================
                      Send FUNCTIONS
    ===============================================*/
   
        $(document).ready(function () {
            $("#login-form").on("submit", function (e) {
                e.preventDefault(); // جلوگیری از ارسال سنتی فرم

                var formData = {
                    PhoneNumber: $("#loginPhoneNumber").val(),
                    Password: $("#loginPassword").val(),
                    RememberMe: $("#rememberMe").is(":checked")
                    
                };
                console.log(formData);
                $.ajax({
                    type: "POST",
                    url: "/Identity/LoginByMobile", // آدرس اکشن
                    data: formData,
                    success: function (response) {
                        if (response.success) {
                            window.location.href = response.redirectUrl; // هدایت به صفحه‌ی اصلی
                            
                        } else {
                            alert(response.message); // نمایش پیام خطا
                        }
                    },
                    error: function () {
                        alert("خطایی در ارتباط با سرور رخ داده است.");
                    }
                });
            });
        });
  

    $(document).ready(function () {
        $("#register-form").on("submit", function (e) {
            e.preventDefault(); // جلوگیری از ارسال سنتی فرم

            var formData = {
                PhoneNumber: $("#registerPhoneNumber").val(),
                DisplayName: $("#registerDisplayName").val(),
                Password: $("#registerPassword").val(),
                RePassword: $("#registerRePassword").val()
            };

            $.ajax({
                type: "POST",
                url: "/Identity/RegisterByMobile", // آدرس اکشن
                data: formData,
                success: function (response) {
                    if (response.success) {
                        window.location.href = response.redirectUrl; // هدایت به صفحه‌ی اصلی
                    } else {
                        alert(response.message); // نمایش پیام خطا
                    }
                },
                error: function () {
                    alert("خطایی در ارتباط با سرور رخ داده است.");
                }
            });
        });
    });


    

    $(document).ready(function () {
        $("#forgot-password-form").on("submit", function (e) {
            e.preventDefault(); // جلوگیری از ارسال سنتی فرم

            var formData = {
                PhoneNumber: $("#forgotPhoneNumber").val()
            };

            $.ajax({
                type: "POST",
                url: "/Identity/ForgotPassword", // آدرس اکشن
                data: formData,
                success: function (response) {
                    if (response.success) {
                        $("#forgot-password-popup").removeClass("active");
                        $("#reset-password-popup").toggleClass("active");
                        $("#resetPhoneNumber").val(response.phoneNumber); // تنظیم شماره تلفن در مودال reset-password-popup
                    } else {
                        alert(response.message); // نمایش پیام خطا
                    }
                },
                error: function () {
                    alert("خطایی در ارتباط با سرور رخ داده است.");
                }
            });
        });
    });



    

    $(document).ready(function () {
        $("#reset-password-form").on("submit", function (e) {
            e.preventDefault(); // جلوگیری از ارسال سنتی فرم

            var formData = {
                Code: $("#resetCode").val(),
                PhoneNumber: $("#resetPhoneNumber").val(),
                Password: $("#resetPassword").val(),
                RePassword: $("#resetRePassword").val()
            };

            $.ajax({
                type: "POST",
                url: "/Identity/RsetPassword", // آدرس اکشن
                data: formData,
                success: function (response) {
                    if (response.success) {
                        $("#reset-password-popup").removeClass("active");
                        $("#sign-popup").toggleClass("active");
                    } else {
                        alert(response.message); // نمایش پیام خطا
                    }
                },
                error: function () {
                    alert("خطایی در ارتباط با سرور رخ داده است.");
                }
            });
        });
    });

    $(document).ready(function () {
        $("#editUserInfo-form").on("submit", function (e) {
            e.preventDefault(); // جلوگیری از ارسال سنتی فرم
           
            var formData = {
                UserId: $("#editUserId").val(),
                DispalyName: $("#editDisplayName").val(),
                RegisterTime: $("#editRegisterTime").val(),
                PhoneNumber: $("#editPhoneNumber").val()
            };
            

            $.ajax({
                type: "POST",
                url: "/UserPanel/UpdateUserInfo", // آدرس اکشن
                data: formData,
                success: function (response) {
                    if (response.success) {
                        /*location.reload();*/
                        window.location.href = response.redirectUrl; // هدایت به صفحه‌ی اصلی
                    } else {
                        alert(response.message); // نمایش پیام خطا
                    }
                },
                error: function () {
                    alert("خطایی در ارتباط با سرور رخ داده است.");
                }
            });
        });
    });


});
