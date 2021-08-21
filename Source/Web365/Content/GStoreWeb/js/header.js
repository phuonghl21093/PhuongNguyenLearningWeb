
const hamburgerBtn = () => {
    const btn = document.querySelector('.hamburger'); 
    const navBar = document.querySelector('.nav__bar');
    const overlay = document.querySelector('.overlay');
    const transformHandler = () => {
        navBar.classList.toggle('list__nav__back');
        overlay.classList.toggle('overlay__block');
        btn.classList.toggle('hamburger__transform');
    }
    btn.addEventListener('click', transformHandler);
    overlay.addEventListener('click', transformHandler);
}
hamburgerBtn();

const navbar = document.querySelector('.nav__bar');
const logo = document.querySelector('.nav__logo img');
const fixedNavbarHandler = () => {
	if (window.pageYOffset > 200) {
		navbar.classList.add('add__class');
		logo.src = '/Content/GStoreWeb/images/img/images/logo-2.png';
		
	}
	else {
		navbar.classList.remove('add__class');
		logo.src = '/Content/GStoreWeb/images/img/images/logo3.png'
		
	}

}
window.addEventListener('scroll', fixedNavbarHandler);

