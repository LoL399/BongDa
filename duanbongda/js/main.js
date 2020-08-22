$(document).ready(function(){
     pos = $("#menuTop").position();
    $(window).scroll(function(){
        var posScroll =$(document).scrollTop();
        if(parseInt(posScroll) > parseInt(pos.top)){
        $("#menuTop").addClass('fixed');
        }
        else{
            $("#menuTop").removeClass('fixed');
        }
    });
});

$(document).ready(function () {
    $('.owl-carousel').owlCarousel({
        loop: true,
        margin: 10,
        // nav: true,
        autoplay:true,
        autoplayTimeout:3000,
        responsiveClass:true,
        responsive:{
            0:{
                items:1,
                // nav:true
            },
            600:{
                items:3,
                // nav:false
            },
            1000:{
                items:5,
                // nav:true,
                loop:true
            }
        }
    })

});

