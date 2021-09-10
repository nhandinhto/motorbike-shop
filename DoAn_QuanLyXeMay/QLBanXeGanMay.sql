Use Master
if exists(Select name from sys.databases where name = 'QLBanXeGanMay')
drop database QLBanXeGanMay
create database QLBanXeGanMay
SET ANSI_WARNINGS OFF
use QLBanXeGanMay
go

create table LoaiTK
(
	MaCV nchar(11) not null,
	TenCV nvarchar(30),
	Constraint PK_loaiTK primary key(MaCV)
)
create table TaiKhoan
(

	Username char(20) not null primary key,
	MK char(20),
	MaNV char(30),
	MaCV nchar(11) not null,
	Constraint FK_CV_TK foreign key (MACV) references LoaiTK(MaCV)
)
create table Nhan_Vien
(
	MaNV char(30) not null primary key,
	HoTenNV nvarchar(100),
	GioiTinh nvarchar(5),
	SoCMND char(20),
	DiaChi nvarchar(100),
	Luong char(20),
)
create table Khach_Hang
(
	MaKH char(20) primary key,
	HoTenKH nvarchar(100),
	GioiTinh nvarchar(5),
	DiaChi nvarchar(50),
	SDT char(20),
	SoCMND char(20)
)
create table Hoa_Don_Ban_Xe
(	
	MaHD char(50) not null primary key,
	MaKH char(20) not null,
	MaNV char(30) not null,
	TongTien money,
	NgayLap date,

)
create table ChiTietHD
(
	MaHD char(50) not null,
	MaXe char(10) not null,
	soluong int,
	donGia money,
	thanhTien bigint,
)
create table Xe
(	
	MaXe char(10) not null,
	MaNCC char(50) not null,
	SoKhung char(10)  not null,
	SoMay char(10) not null,
	TenXe nvarchar(20),
	MauSac nvarchar(20),
	Gia money,
	hinh nchar(50),
	soluong int,
	primary key (MaXe)
)
create table Hoa_Don_Nhap_Xe
(

	MaHDN char(50) not null,
	MaNCC char(50) not null,
	MaNV char(30) not null,
	ThanhTien char(50),
	SoLuong int,
	NgayLap datetime,
	hinh image,
	primary key(MaNCC,MaHDN)
)
create table Nha_Cung_Cap
(
	MaNCC char(50) not null primary key,
	TenNCC nvarchar(50),
	DiaChi nvarchar(100),
	SDT char(20),
	Gmail char(100)
)
create table Bao_Hanh
(	
	MaBH char(20) not null,
	MaXe char(10) not null,
	MaKH char(20) not null,
	TenKH nvarchar(100),
	TenXe nchar(20),
	SoKhung char(20),
	SoMay char(20),
	ThoiGianBH char(20),
	hinh image,
	primary key(MaBH,MaXe)
)
create table Phieu_Nhap_Hang
(
	
	MaNV char(30) not null,
	MaNCC char(50) not null,
	MaPhieuNhap char(20) not null,
	SoLuong int,
	DonGia char(50),
	NgayLap char(20),
	TongTien char(50),
	hinh image,
	primary key (MaNCC,MaPhieuNhap)
)
alter table Xe
add constraint FK_Xe_NCC foreign key (MaNCC) references Nha_Cung_Cap(MaNCC)
alter table Hoa_Don_Ban_Xe
add constraint FK_MaNV_HDBan foreign key(MaNV) references Nhan_Vien(MaNV)
alter table Hoa_Don_Nhap_Xe
add constraint FK_MaNV_HDNhap foreign key(MaNV) references Nhan_Vien(MaNV)
alter table TaiKhoan
add constraint FK_MaNV_Login foreign key(MaNV) references Nhan_Vien(MaNV)
alter table Phieu_Nhap_Hang
add constraint FK_MaNV_PhieuNhap foreign key(MaNV) references Nhan_Vien(MaNV)
alter table Hoa_Don_Ban_Xe
add constraint FK_MaKH_HDBan foreign key(MaKH) references Khach_Hang(MaKH)
alter table Bao_Hanh
add constraint FK_MaKH_BH foreign key(MaKH) references Khach_Hang(MaKH)
alter table Bao_Hanh
add constraint FK_MaXe_BH foreign key(MaXe) references Xe(MaXe)
alter table Hoa_Don_Ban_Xe
add constraint Default_TongTien Default (0) for TongTien
------------------------tẠO tRIGGER
go
create trigger update_gia on ChiTietHD
for Update,Insert
as
begin
	Update ChiTietHD
	Set donGia = (Select Gia from Xe,inserted where Xe.MaXe = inserted.MaXe)
	from ChiTietHD,inserted
	where ChiTietHD.MaHD = inserted.MaHD and ChiTietHD.MaXe = inserted.MaXe

	update ChiTietHD
	set thanhTien = (Select ChiTietHD.donGia from ChiTietHD,inserted where ChiTietHD.MaHD = inserted.MaHD and ChiTietHD.MaXe = inserted.MaXe) * (Select ChiTietHD.soluong from ChiTietHD,inserted where ChiTietHD.MaHD = inserted.MaHD and ChiTietHD.MaXe = inserted.MaXe)
	from ChiTietHD,inserted
	where ChiTietHD.MaHD = inserted.MaHD and ChiTietHD.MaXe = inserted.MaXe
end

go
create trigger update_tien_hoadon on ChiTietHD
after insert,update,delete
as
begin
		Update Hoa_Don_Ban_Xe
		set TongTien = (Select Sum(ChiTietHD.thanhTien) from ChiTietHD join inserted on ChiTietHD.MaHD = inserted.MaHD join Hoa_Don_Ban_Xe on ChiTietHD.MaHD = Hoa_Don_Ban_Xe.MaHD)
		from Hoa_Don_Ban_Xe join inserted on Hoa_Don_Ban_Xe.MaHD = inserted.MaHD
end


go

--------------Nhập liệu

insert into LoaiTK
values
('CV02',N'Nhân Viên'),
('CV01',N'Quản lý')

insert into Nhan_Vien
values
('NV01',N'Nguyễn Huy Khôi Nguyên',N'Nam','0123456789101',N'Quận 12','6000000'),
('NV02',N'Nguyễn Thị Kim Nhung',N'Nữ','0123456789102',N'Quận Tân Phú','10000000'),
('QL01',N'Nguyễn Hoàng Quý',N'Nam','0123456789103',N'Quận Bình Tân',NULL),
('QL02',N'Tô Đình Nhân',N'Nam','0123456789104',N'Quận 4',NULL)

insert into TaiKhoan
values
('nguyenwibu','1','NV01',N'CV02'),
('nhungwibu','1','NV02',N'CV02'),
('QQQs','1','QL01',N'CV01'),
('NNNs','1','QL02',N'CV01')

insert into Khach_Hang
values
('KH01',N'Nguyễn Văn Thảo',N'Nam',N'Quận Bình Tân','0932055472','0123456789105'),
('KH02',N'Vũ Hoàng Thiên Ân',N'Nam','Quận Bình Tân','0932055473','0123456789106')

insert into Nha_Cung_Cap
values
('HD','HonDa','Japanese','0903671283','TokudaShigeo@gmail.com'),
('YM','Yamaha','Japanese','0904421875','MariaOzawa@gmail.com')

insert into Xe
values
('XE01','HD','HD010114','56735','Wave RSX FI 110',N'Đỏ',21690000,'waveRSX.jpg',1),
('XE02','HD','HD010115','56736','Wave Alpha FI 110',N'Trắng',17600000,'WaveAlpha.jpg',1),
('XE03','HD','HD010116','56737','SH Mode 125',N'Đỏ',53890000,'SHMode.jpg',1),
('XE04','HD','HD010117','56738','ReBel 500',N'Đen',180000000,'Rebel500.jpg',1),
('XE05','HD','HD010118','56739','Winner X',N'Đỏ',45990000,'WinnerX.jpg',1),
('XE06','HD','HD010119','56740','CBR1000RR',N'Đỏ',1049000000,'CBR1000R.jpg',1),
('XE07','HD','HD010120','56741','CBR1000RRF',N'Đỏ',949000000,'CBR1000RF.png',1),
('XE08','HD','HD010121','56742','SH 150i',N'Đỏ',70990000,'SH150i.png',1),
('XE09','HD','HD010122','56743','Goldwing',N'Trắng',1200000000,'GoldWing.jpg',1),
('XE10','HD','HD010123','56744','Future 125FI',N'Trắng',30190000,'Future125i.jpg',1),
('XE11','HD','HD010124','56745','CB150R',N'Đỏ',105000000,'CBR150R.jpg',1),
('XE12','HD','HD010125','56746','PCX 150',N'Trắng',56490000,'PCX150.jpg',1),
('XE13','HD','HD010126','56747','MSX125',N'Đỏ',49990000,'MSX125.jpg',1),
('XE14','HD','HD010127','56748','Rebel 300',N'Đen',125000000,'Rebel300.jpg',1),
('XE15','HD','HD010128','56749','CB500X',N'Đen',187990000,'CB500X.jpg',1),
('XE16','HD','HD010129','56750','Rebel 500',N'Xám',180000000,'Rebel500.jpg',1),
('XE17','HD','HD010130','56751','CBR500R',N'Đỏ',186990000,'CBR500.jpg',1),
('XE18','HD','HD010131','56752','CB300R',N'Đỏ',140000000,'CB300R.jpg',1),
('XE19','HD','HD010132','56753','CBR650R',N'Đỏ',253990000,'CBR650R.jpg',1),
('XE20','HD','HD010133','56754','CB650R',N'Đỏ',245990000,'CB650R.jpg',1),
('XE21','HD','HD010134','56755','CB1000R',N'Đen',468000000,'CB1000R.jpg',1),
('XE22','HD','HD010135','56756','CB500F',N'Đen',178990000,'CB500F.jpg',1),
('XE23','HD','HD010136','56757','Air Blade',N'Xám',41190000,'AirBlade.png',1),
('XE24','HD','HD010137','56758','Vision',N'Đen',29990000,'Vision.jpg',1),
('XE25','HD','HD010138','56759','Lead 125',N'Đỏ',38290000,'Lead.jpg',1),
('XE26','HD','HD010139','56760','SH300i ABS',N'Đỏ',276490000,'SH300i.jpg',1),
('XE27','HD','HD010140','56761','Blade',N'Xanh',18800000,'Blade.jpg',1),
('XE28','HD','HD010141','56762','Monkey',N'Vàng',84990000,'Monkey.jpg',1),
('XE29','HD','HD010142','56763','Super Cub',N'Xanh',84990000,'SuperCub.jpg',1),
('XE30','HD','HD010143','56764','PCX Hybrid',N'Xanh',89990000,'PCXHybrid.png',1),
('XE31','YM','YM010111','67846','Exciter 150',N'Đen',48990000,'Exciter150.jpg',1),
('XE32','YM','YM010112','67847','Jupiter FI',N'Xanh',30000000,'JupiterGP.jpg',1),
('XE33','YM','YM010113','67848','Sirius FI',N'Đen',23190000,'SiriusFI.jpg',1),
('XE34','YM','YM010114','67849','Sirius RC',N'Xám',21300000,'SiriusRC.jpg',1),
('XE35','YM','YM010115','67850','Grande',N'Trắng',50000000,'Grande.jpg',1),
('XE36','YM','YM010116','67851','Latte',N'Trắng',37990000,'Latte.jpg',1),
('XE37','YM','YM010117','67852','Janus',N'Trắng',31990000,'Janus.jpg',1),
('XE38','YM','YM010118','67853','NVX',N'Đen',54000000,'NVX.jpg',1),
('XE39','YM','YM010119','67854','FreeGo',N'Đen',38990000,'FreeGo.jpg',1),
('XE40','YM','YM010120','67855','Acruzo',N'Xanh Rêu',33490000,'Acruzo.jpg',1),
('XE41','YM','YM010121','67856','R3',N'Xanh',129000000,'R3.jpg',1),
('XE42','YM','YM010122','67857','MT03',N'Đen',124000000,'MT03.jpg',1),
('XE43','YM','YM010123','67858','R15',N'Đen',72000000,'R15.jpg',1),
('XE44','YM','YM010124','67859','MT15',N'Xanh',69000000,'MT15.jpg',1),
('XE45','YM','YM010125','67860','TFX150',N'Đen',72900000,'TFX150.jpg',1),
('XE46','YM','YM010126','67861','Exciter 135',N'Xanh',49000000,'Exciter135.jpg',1),
('XE47','YM','YM010127','67862','Dream',N'Trắng',40000000,'Dream.jpg',1),
('XE48','YM','YM010128','67863','Vario',N'Đen',60000000,'Vario.jpg',1),
('XE49','YM','YM010129','67864','R1',N'Xanh',900000000,'R1.jpg',1),
('XE50','YM','YM010130','67865','R6',N'Xanh',650000000,'R6.jpg',1)

set DateFormat DMY
insert into Hoa_Don_Ban_Xe
values
('HD01','KH01','NV01',0,'2020-05-20'),
('HD02','KH02','NV02',0,'2020-06-20')

insert into ChiTietHD
values
('HD01','XE01',1,null,null)

insert into Hoa_Don_Nhap_Xe
values
('HDN01','HD','NV01',1000000000,20,'2020-01-01 00:00:00','GoldWing.jpg'),
('HDN02','YM','NV02',1250000000,25,'2020-01-01 00:00:00','SuperCub.jpg')
insert into Bao_Hanh
values
('BH01','XE01','KH01',N'Nguyễn Văn Thảo','Wave RSX FI 110','HD010114','56735',N'2 ngày','waveRSX.jpg'),
('BH02','XE50','KH02',N'Vũ Hoàng Thiên Ân','R6','YM010130','67865',N'7 ngày','R6.jpg')
--insert into Phieu_Nhap_Hang
--values
--('NV01','HD','PN01',20,50000000,'2020-01-01 00:00:00',1000000000,'GoldWing.jpg'),
--('NV02','YM','PN01',25,50000000,'2020-01-01 00:00:00',1250000000,'SuperCub.jpg')


--------------------Test chức năng
select * from Hoa_Don_Ban_Xe

select * from ChiTietHD

Update ChiTietHD
Set soluong = 1
Where MaHD = 'HD01' AND MaXe = 'XE01'

update Hoa_Don_Ban_Xe
set TongTien = 0
where MaHD = 'HD01'

insert into ChiTietHD
values
('HD01','XE02',1,null,null),
('HD01','XE03',2,null,null)
