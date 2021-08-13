const Header = () => {
    return '<div class="overlay"></div><nav class="nav__bar"><div class="nav__logo"><img src="/Content/GstoreWeb/images/img/images/logo3.png"/></div><ul class="list__nav" id="nav"><li><a href="/">Trang chủ</a></li><li><a href="">Tính năng</a></li><li><a href="">Giới thiệu</a></li><li><a href=">Màn hình</a></li><li><a href=">Về chúng tôi</a></li><li><a href=">Ứng dụng</a></li><li class="><a href=">Tin tức</a><div class="list__types"><p><a href=">Tin nội bộ</a></p><p><a href="">Tin công nghệ</p><p><a href="">Tin tuyển dụng</a></p></div></li></ul><div class="hamburger"><div class="ham1"></div><div class="ham1"></div><div class="ham1"></div></div></nav>';
};

const navBar = document.getElementById('header');
navBar.innerHTML = Header();
// su dung header: 
// step 1: dat tag <div id='header'></div> vao html
// step 2: add file header.js o ben folder js
// step 3: add scss header.scss ben folder scss vao
// step 4: enjoy! done