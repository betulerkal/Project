create table Urunler
(
UrunId int identity(1,1) not null,
UrunAdi nvarchar(250) not null,
UrunAciklamasi nvarchar(max),
UrunFiyati money not null,
KategoriId int not null
)

create table Kategoriler
(
KategoriId int identity(1,1) not null,
KategoriAdi nvarchar(150) not null
)

create table Kullanicilar
(
ID int identity(1,1) not null,
Ad nvarchar(25),
Soyad nvarchar(25),
KullaniciAdi nvarchar(25) not null,
Email nvarchar(70) not null,
Sifre nvarchar(25) not null
)

go

--Ürünler Prosedür--
create proc UrunEkle
@UrunId int,
@UrunAdi nvarchar(250),
@UrunAciklamasi nvarchar(max),
@UrunFiyati money,
@KategoriId int
as
insert Urunler
values(@UrunAdi,@UrunAciklamasi,@UrunFiyati,@KategoriId)
go

create proc UrunDuzenle
@UrunId int,
@UrunAdi nvarchar(250),
@UrunAciklamasi nvarchar(max),
@UrunFiyati money,
@KategoriId int
as
update Urunler set UrunAdi=@UrunAdi,UrunAciklamasi=@UrunAciklamasi,UrunFiyati=@UrunFiyati
where UrunId=@UrunId
go

create proc UrunSil
@UrunId int
as
delete Urunler where UrunId=@UrunId
go

create proc UrunListele
as
select * from Urunler
go

--Kategoriler Prosedür--
create proc KategoriEkle
@KategoriId int,
@KategoriAdi nvarchar(150)
as
insert Kategoriler
values(@KategoriAdi)
go

create proc KategoriDuzenle
@KategoriId int ,
@KategoriAdi nvarchar(150)
as
update Kategoriler set KategoriAdi=@KategoriAdi
where KategoriId=@KategoriId
go

create proc KategoriSil
@KategoriId int
as
delete Kategoriler where KategoriId=@KategoriId
go

create proc KategoriListele
as
select * from Kategoriler
go