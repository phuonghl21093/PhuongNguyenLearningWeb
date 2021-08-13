const helperFunction = (overlay, content) => {
    overlay.classList.toggle('overlay__block');
    content.classList.toggle('download__links__block');
}
const buttonHandler = () => {
    const btn = document.querySelector(".btn__dl");
    const btnClose = document.querySelector('.btn__close');
    const overlay = document.querySelector(".overlay__primary");
    const content = document.querySelector('.download__links');

    const btn2 = document.querySelector('.btn__dl-2');
    const btnClose2 = document.querySelector('.btn__close-2')
    const content2 = document.querySelector('.download__links-2')
    btn.addEventListener('click', () => helperFunction(overlay, content));
    btnClose.addEventListener('click', () => helperFunction(overlay, content));
    btn2.addEventListener('click', () => helperFunction(overlay, content2));
    btnClose2.addEventListener('click', () => helperFunction(overlay, content2));
};
buttonHandler();