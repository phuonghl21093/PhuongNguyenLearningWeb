const onPlayHandler = () =>{
    const btn = document.querySelector('.introduction__button');
    const video = document.getElementById('video__intro');
    const onplayVideoHandler = () =>{
        video.src = video.src + '?autoplay=1'
    }
    btn.addEventListener('click', onplayVideoHandler)
}
onPlayHandler();