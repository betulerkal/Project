
    $("#Load").html("Yükleniyor...");
    $.get("/Urunler/GetUrunList", null, DataBind)

    function DataBind(liste) {
        var setData = $("#SetUrunList");
        for (var i = 0; i < liste.length; i++) {
            var data = "<tr>" +
            "<td>" + liste[i].UrunAdi + "</td>" +
                "<td>" + liste[i].UrunAciklamasi + "</td>" +
                "<td>" + liste[i].UrunFiyati + "</td>" +
                "<td>" + liste[i].KategoriAdi + "</td>" +
            "<td>" +
            "<button class='btn btn-warning btn-xs' onclick='EditUrun(" + liste[i].UrunId + ")' title='Düzenle' >" +
                "<span class='glyphicon glyphicon-edit'>" + "</span>" +
            "</button>" + " " +
            "<button class='btn btn-danger btn-xs' onclick='DeleteUrun(" + liste[i].UrunId + ")' title='Sil' >" +
                "<span class='glyphicon glyphicon-trash'>" + "</span>" +
            "</button>" +
            "</td>" +
            "</tr>";
            setData.append(data);
            $("#Load").html(" ");
        }
    }
    function EditUrun(UrunId) {
        var url = "/Urunler/GetEditDeleteUrunler?UrunId=" + UrunId;
        $("#ModalDuzenle").modal();
        $.ajax({
            url: url,
            type: "get",
            warning: function (data) {
                //var obj = JSON.parse(data);
                $("#id").val(data.UrunId);
                $("#urunad").val(data.UrunAdi);
                $("#aciklama").val(data.UrunAciklamasi);
                $("#fiyat").val(data.UrunFiyati);
                $("#katid").val(data.KategoriId);
            }
        });
    }
    function DeleteUrun(UrunId) {
        var url = "/Urunler/GetEditDeleteUrunler?UrunId=" + UrunId;
        $("#ModalSil").modal();
        $.ajax({
            url: url,
            type: "get",
            warning: function (data) {
                //var obj = JSON.parse(data);
                $("#id2").val(data.UrunId);
                $("#urunad2").val(data.UrunAdi);
                $("#aciklama2").val(data.UrunAciklamasi);
                $("#fiyat2").val(data.UrunFiyati);
                $("#katid2").val(data.KategoriId);
            }
        });
    }



//$.ajax({
//    method: "GET",
//    url: '@Url.Action("SehirGetir","Home")' + '/' + ulkeID
//}).done(function (result) {
//    for (var i = 0; i < result.length; i++) {
//        var sehiradi = result[i].SehirAdi;
//        var sehirid = result[i].SehirID;
//        var opt = $("<option></option>");
//        opt.text(sehiradi);
//        opt.val(sehirid);

//        $("#SecilenSehir").append(opt);   //ekledik.
//    }
//})