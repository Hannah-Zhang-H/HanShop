document.addEventListener('DOMContentLoaded', function () {
    //-----------------------------------------------------------------------------------------------
    // For "/Home/Detail" page -> product details display part -> magnifier 
    //-----------------------------------------------------------------------------------------------

    const magnifierImg = document.querySelector("#magnifierImg");
    const magnifier = document.querySelector("#magnifier");


    /*a black mask will be displayed while the mouse is moving*/
    const mask = document.querySelector("#mask");
    magnifierImg.addEventListener('mouseenter', function () {
        mask.style.display = 'block';
        magnifier.style.display = "block";
    })


    magnifierImg.addEventListener('mouseleave', function () {

        mask.style.display = 'none';
        magnifier.style.display = "none";
    })


    // move mask on the Image
    magnifierImg.addEventListener('mousemove', function (e) {
        // mouse position in the magnifierImg = mouse position in the page - magnifierImg position
        let x = e.pageX - magnifierImg.getBoundingClientRect().left;
        let y = e.pageY - magnifierImg.getBoundingClientRect().top - document.documentElement.scrollTop;
        // the mask can only move in x->480 y->380 area
        if (x >= 0 && x <= 480 && y >= 0 && y <= 380) {
         // however, the mask is not always moving in the magnifierImg.
            let mx = 0, my = 0;
            if (x < 120) mx = 0; // mask not moving
            if (x >= 120 && x <= 420) mx = x -120;
            if (x > 420) mx = 420;

            if (y < 120) my = 0; // mask not moving
            if (y >= 120 && y <= 238) my = y - 120;
            if (y > 238) my = 238;
            mask.style.left = mx + 'px';
            mask.style.top = my + 'px';

        }
        //magnifier backgroundImg should move while the mask is moving.
        magnifier.style.backgroundPositionX = -0.6 * x + 'px';
        magnifier.style.backgroundPositionY = -0.6 * y + 'px';

    })
})

