$(function () {


    //HEADER
    $(window).scroll(function () {
          if($(this).scrollTop() > 200)
          {
              if (!$('.main_header').hasClass('fixed'))
              {
                  $('.main_header').stop().addClass('fixed').css('top', '-100px').animate(
                      {
                          'top': '0px'
                      }, 500);
              }
          }
          else
          {
              $('.main_header').removeClass('fixed');
          }
    });


});

$(window).load(function() { // Garante que todo o site seja carregado
  $('#status').fadeOut(); // primeiro desaparecerá a animação de carregamento
  $('#preloader').delay(350).fadeOut('slow'); // desaparecerá o DIV branco que cobre o site.
  $('body').delay(350).css({'overflow':'visible'});
})