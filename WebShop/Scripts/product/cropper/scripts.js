$(function () {
    var cropper = function () {
        //Чи почали процес кропання малюнка
        var isCropped = false;
        var $inputFileImage = $("#img_file");
        var counterImages = 1;
        var changeImage = 0;
        var loader = {
            show: function () {
                $("#loading").show();
            },
            hide: function () {
                $("#loading").hide();
            }
        };
        //Діалогове вікно для обрізки фото
        var dialog = {
            body: document.querySelector('body'),
            show: function () {
                this.body.classList.toggle("open");
                $(".containerCrop").show();
                $(".navbar").hide();
            },
            hide: function () {
                this.body.classList.remove("open");
                $(".containerCrop").hide();
                $(".navbar").show();
            }
        };
        var server = {
            sendImage: function (base64) {
                var form = $('#__AjaxAntiForgeryForm');
                var token = $('input[name="__RequestVerificationToken"]', form).val();
                var imageCurrent = "#containerimage" + counterImages;

                var image = $(imageCurrent).find(".uploadimage")[0];
                $.ajax({
                    url: "/ProductImages/UploadBase64",
                    type: "POST",
                    data: {
                        __RequestVerificationToken: token,
                        base64image: base64 
                    },
                    success: function (result) {
                        if (changeImage !== counterImages) {
                            imageCurrent = "#containerimage" + changeImage;
                            image = $(imageCurrent).find(".uploadimage")[0];
                            image.src = result.imagePath;
                            dialog.hide();
                            //$inputFileImage.replaceWith($inputFileImage.val('').clone(true));
                            isCropped = false;
                            return;
                        }

                        image.src = result.imagePath;
                        //console.log();
                    }
                });
            }
        };
        function init() {
            initButtonControls();
            initEventBaseClick();

            //Базові кнопакі діалога
            function initEventBaseClick() {
                //Закрити діалог кропера
                $("#cropperClose").on("click", function () {
                    dialog.hide();
                });
                //Вибір нового малюнка
                $inputFileImage.on('change', function () {
                    if (this.files && this.files[0]) {
                        if (this.files[0].type.match(/^image\//)) {
                            onLoad(this.files[0]);
                        }
                        else {
                            alert("Invalid file type");
                        }
                    }
                    else {
                        alert("Please select a file.");
                    }

                });
                //Обрізати фото натиснули
                $("#crop").click(function () {
                    cropped();
                });
                //Натиснути на плюс або фотку
                $("#listUploadImages").on('click', '.plusupload', function () {
                    changeImage = $(this).data("counter");
                    $inputFileImage.click();
                    return false;
                });
                $("#listUploadImages").on('click', '.icon-delete', function () {
                    var item = $(this);
                    var p = item.closest(".plusupload");
                    console.log(changeImage = p.data("counter"));
                    var id = "#containerimage" + p.data("counter");
                    $(id).remove();
                    return false;
                });
            }

            //Ініціалізація конопок самого кропера поворот
            function initButtonControls() {

                $("#dragModeMove").on("click", function () {
                    $('#canvas').cropper('setDragMode', 'move');
                });

                $("#dragModeCrop").on("click", function () {
                    $('#canvas').cropper('setDragMode', 'crop');
                });
                var x = 1;
                $("#scaleX").on("click", function () {
                    $('#canvas').cropper('scaleX', x *= -1);

                });
                var y = 1;
                $("#scaleY").on("click", function () {
                    $('#canvas').cropper('scaleY', y *= -1);
                });

                $("#rotateLeft").on("click", function () {
                    $('#canvas').cropper('rotate', 45);
                });

                $("#rotateRight").on("click", function () {
                    $('#canvas').cropper('rotate', -45);
                });

                $("#moveLeft").on("click", function () {
                    $('#canvas').cropper('move', -50, 0);
                });

                $("#moveRight").on("click", function () {
                    $('#canvas').cropper('move', 50, 0);
                });

                $("#moveUp").on("click", function () {
                    $('#canvas').cropper('move', 0, -50);
                });

                $("#moveDown").on("click", function () {
                    $('#canvas').cropper('move', 0, 50);
                });

                $("#zoomPlus").on("click", function () {
                    $('#canvas').cropper('zoom', 0.1);
                });

                $("#zoomMinus").on("click", function () {
                    $('#canvas').cropper('zoom', -0.1);
                });
            }
        }

        function crop() {
            init();
        }
        //Загрузка малюнка в кропер
        function onLoad(fileImage) {
            var $canvas = $("#canvas"),
                context = $canvas.get(0).getContext('2d');
            //Включити -----------
            loader.show();
            var reader = new FileReader();
            reader.onload = function (e) {
                //Виключити ---------
                var img = new Image();
                img.onload = function () {

                    loader.hide();
                    context.canvas.width = img.width;
                    context.canvas.height = img.height;

                    if (img.width <= 300 && img.height <= 300) {
                        alert("Ображення менше 300 пікселів");
                        return;
                    }
                    //Показуємо діалог
                    dialog.show();
                    isCropped = true;

                    context.drawImage(img, 0, 0);
                    var cropper = $canvas.cropper('destroy').cropper({
                        aspectRatio: 1 / 1,
                        viewMode: 1,
                        dragMode: 'move',
                        preview: '.img-preview',
                        //autoCropArea: 0.00000001,
                        //aspectRatio: 2,
                        //,
                        crop: function (e) {
                            var data = e.detail;
                            var h = Math.round(data.height);
                            var w = Math.round(data.width);
                            if (w <= 300) {
                                this.cropper.setData({ width: 300 });
                            }
                            //else
                        }
                    });
                };
                img.src = e.target.result;
            };
            reader.readAsDataURL(fileImage);
        }

        function cropped() {
            if (isCropped) {
                var $canvas = $("#canvas");
                var croppedImage = $canvas.cropper('getCroppedCanvas').toDataURL('image/jpg');
                server.sendImage(croppedImage.split(',')[1]);
                //$('#result').html($('<img>').attr('src', croppedImage));
                //console.log(croppedImage);
                //Зображення обрізане записуємо у скрите поле на формі
                
                //$("#imgSelectView").attr("src", croppedImage);
                //$('#ImageBase64').attr("value", croppedImage.split(',')[1]);

                $('#del' + counterImages).show();
                //Завантажили одне фото на сайт
                counterImages++;
                var itemAddImage = '';
                itemAddImage += '<div class="col-md-2 plusupload" id="containerimage'
                    + counterImages + '"  data-counter="' + counterImages + '">';
                itemAddImage += '<div class="thumbnail">';
                itemAddImage += '<i class="fa fa-times fa-2x icon-delete" aria-hidden="true" id="del' + counterImages + '"></i>';
                itemAddImage += '<img src="' + PathImage + '" class="uploadimage" alt = "Lights" style = "width:100%" >';
                itemAddImage += '<div class="caption"><p>Add New Image</p></div>';
                itemAddImage += '</div>';
                itemAddImage += '</div>';

                $("#listUploadImages").append(itemAddImage);
                dialog.hide();
                //$inputFileImage.replaceWith($inputFileImage.val('').clone(true));
                isCropped = false;
            }
        }
        return { crop: crop };
    }();

    cropper.crop();
});