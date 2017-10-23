using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;

namespace WebServiceHaliStok
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {
        private static string GetCs()
        {
            string cs = @"Data Source=DESKTOP-C5NGNKE\SB;Initial Catalog=HALI;Integrated Security=SSPI;User ID = SA;Password=123456;";
            return cs;
        }
        //giriş yaparken kullanıcı bilgilerini kontrol ediyor
        [WebMethod]
        public DataSet kullaniciGiris(string kullaniciAdi,string kullaniciKodu, string kullaniciSifre)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            SqlConnection conn = new SqlConnection(GetCs());
            string query = "SELECT subeID from KULLANICILAR WHERE kullaniciAdi='" + kullaniciAdi + "' and kullaniciKodu='" + kullaniciKodu + "' and kullaniciSifre='" + kullaniciSifre + "' ";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;

        }
        [WebMethod]
        public DataSet urunAra(string urunAdi)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            string cs = GetCs();
            SqlConnection conn = new SqlConnection(GetCs());
            string query = "select  ITEMCODE as Ürün_Kodu, ITEMNAME as Ürün_Adı, FABRIC as Malzemesi, COLOR as Renk, DIMENSION as Ölçü, SHAPE as Şekil, CONTOUR as Hat FROM ITEMS WHERE ITEMNAME like '%" + urunAdi + "%'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;

        }
        /* Kullanıcı adlarını listeleyerek seçim yapılması düşünüldü ama kullanılmadı
        [WebMethod]
        public DataSet kullaniciAdiCek()
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            string cs = GetCs();
            SqlConnection conn = new SqlConnection(GetCs());
            string query = "select kullaniciAdi FROM KULLANICILAR";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;

        }*/
        //satışları listeler
        [WebMethod]
        public DataSet satisListe()
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            string cs = GetCs();
            SqlConnection conn = new SqlConnection(GetCs());
            string query = "SELECT TOP 100 [ITEMCODE] AS URUNKODU ,[ITEMNAME] AS URUNADI,[FABRIC] AS MALZEME,[COLOR] AS RENK,[DIMENSION] AS OLCU ,[SHAPE] AS SEKİL,[CONTOUR] AS DOKU FROM [HALI].[dbo].[ITEMS]";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;

        }
        [WebMethod]
        public System.Data.DataSet barcodeBilgisi(string barcode)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            string cs = GetCs();
            SqlConnection conn = new SqlConnection(cs);
            string query = "select BRC.BARCODE,ITM.ITEMCODE,ITM.ITEMNAME,ITM.COLOR,ITM.CONTOUR,ITM.SHAPE,ITM.FABRIC,BRC.STATUS_ from BARCODES BRC,ITEMS ITM WHERE BRC.ITEMCODE=ITM.ITEMCODE AND Barcode='"+ barcode +"'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        [WebMethod]
        public DataSet musteriCek()
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            string cs = GetCs();
            SqlConnection conn = new SqlConnection(GetCs());
            string query = "select MSR.musteriKodu AS [MÜŞTERİ KODU],MSR.musteriAdSoyad AS [AD SOYAD], ADR.ulke AS [ÜLKE],ADR.sehir AS [ŞEHİR],ADR.ilce AS [İLÇE],ADR.semt AS SEMT,ADR.postaKodu AS [POSTA KODU],MSR.musteriTel1 AS [TELEFON NUMARASI 1],MSR.musteriTel2 AS [TELEFON NUMARASI 2]"
            +"from MUSTERILER MSR, ADRES ADR where MSR.adresID1=ADR.ID";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;

        }
        [WebMethod]
        public DataSet musteriAdiAra(string musteriAdi)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            string cs = GetCs();
            SqlConnection conn = new SqlConnection(GetCs());
            string query = "select MSR.musteriKodu AS [MÜŞTERİ KODU],MSR.musteriAdSoyad AS [AD SOYAD], ADR.ulke AS [ÜLKE],ADR.sehir AS [ŞEHİR],ADR.ilce AS [İLÇE],ADR.semt AS SEMT,ADR.postaKodu AS [POSTA KODU],MSR.musteriTel1 AS [TELEFON NUMARASI 1],MSR.musteriTel2 AS [TELEFON NUMARASI 2] "
            + " from MUSTERILER MSR, ADRES ADR where MSR.adresID1=ADR.ID AND MSR.musteriAdSoyad like '%" + musteriAdi + "%'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;

        }
        /*
        [WebMethod]
        public DataSet musteriCagir(int id)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            string cs = GetCs();
            SqlConnection conn = new SqlConnection(GetCs());
            string query = "select MSR.musteriKodu ,MSR.musteriAdSoyad , ADR.ulke,ADR.sehir,ADR.ilce,ADR.semt,ADR.postaKodu,MSR.musteriTel1,MSR.musteriTel2 from MUSTERILER MSR, ADRES ADR where MSR.adresID1=ADR.ID AND MSR.ID like '%" + id + "%'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;

        }*/
        [WebMethod]
        public System.Data.DataSet musteriEkle(string musteriAdSoyad, string adres1Ulke, string adres1Sehir, string adres1Ilce, string adres1Semt, string adres1PostaKodu,
            string adres2Ulke, string adres2Sehir, string adres2Ilce, string adres2Semt, string adres2PostaKodu, string musteriTel1, string musteriTel2, string TCKimlikVergiNo, string vergiDairesi)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            string cs = GetCs();
            SqlConnection conn = new SqlConnection(cs);
            string query = "begin tran"
            + " INSERT INTO ADRES(ulke, sehir, ilce, semt, postaKodu) VALUES ('"+adres1Ulke+"','"+adres1Sehir+"','"+adres1Ilce+"','"+adres1Semt+"','"+adres1PostaKodu+"')"
            +" DECLARE @AdresId1 INT"
            +" SET @AdresId1 = SCOPE_IDENTITY()"
            +" INSERT INTO ADRES(ulke, sehir, ilce, semt, postaKodu) VALUES ('"+adres2Ulke+"','"+adres2Sehir+"','"+adres2Ilce+"','"+adres2Semt+"','"+adres2PostaKodu+"')"
            +" DECLARE @AdresId2 INT"
            +" SET @AdresId2 = SCOPE_IDENTITY()"
            +" DECLARE @rand_no int"
            +" SET  @rand_no= (SELECT TOP 1 ABS(CAST(NEWID() AS BINARY(6)) %1000) + 1 FROM   sysobjects)"
            + " INSERT INTO MUSTERILER( musteriKodu, musteriAdSoyad, adresID1, adresID2, musteriTel1, musteriTel2, TCKimlikVergiNo, vergiDairesi) VALUES (@rand_no,'" + musteriAdSoyad + "',@AdresId1,@AdresId2,'" + musteriTel1 + "','" + musteriTel2 + "','" + TCKimlikVergiNo + "','" + vergiDairesi + "')"
            +" commit tran";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        [WebMethod]
        public System.Data.DataSet stoklardaBarkodAra(string barcode)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            string cs = GetCs();
            SqlConnection conn = new SqlConnection(cs);
            string query = "select BRC.BARCODE AS BARKOD,ITM.ITEMNAME AS URUNADI,ITM.COLOR AS RENK,ITM.SHAPE AS SEKIL,ITM.FABRIC AS DOKU,BRC.STATUS_  AS DURUM,STK.stokAdeti AS STOKADETI,STK.stokDurumu AS STOKDURUMU,SB.subeAdi AS SUBEADI,SB.telefon AS TELEFON"
            + " from BARCODES BRC,ITEMS ITM,STOK STK,SUBELER SB WHERE BRC.ITEMCODE=ITM.ITEMCODE AND BRC.BARCODE=STK.urunBarkod AND SB.ID=STK.subeID AND BARCODE='" + barcode + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        [WebMethod]
        public System.Data.DataSet stoklardaUrunAdiAra(string urunAdi)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            string cs = GetCs();
            SqlConnection conn = new SqlConnection(cs);
            string query = "select DISTINCT BRC.BARCODE AS BARKOD,ITM.ITEMNAME AS URUNADI,ITM.COLOR AS RENK,ITM.SHAPE AS SEKIL,ITM.FABRIC AS DOKU,BRC.STATUS_  AS DURUM,STK.stokAdeti AS STOKADETI,STK.stokDurumu AS STOKDURUMU,SB.subeAdi AS SUBEADI,SB.telefon AS TELEFON"
            + " from BARCODES BRC,ITEMS ITM,STOK STK,SUBELER SB WHERE BRC.ITEMCODE=ITM.ITEMCODE AND BRC.BARCODE=STK.urunBarkod AND SB.ID=STK.subeID AND ITM.ITEMNAME like '%" + urunAdi + "%'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        [WebMethod]
        public System.Data.DataSet subeStokListele(string subeAdi)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            string cs = GetCs();
            SqlConnection conn = new SqlConnection(cs);
            string query = "select DISTINCT  BRC.BARCODE AS BARKOD,ITM.ITEMNAME AS URUNADI,ITM.COLOR AS RENK,ITM.SHAPE AS SEKIL,ITM.FABRIC AS DOKU,BRC.STATUS_  AS DURUM,STK.stokAdeti AS STOKADETI,STK.stokDurumu AS STOKDURUMU,SB.subeAdi AS SUBEADI,SB.telefon AS TELEFON"
            + " from BARCODES BRC,ITEMS ITM,STOK STK,SUBELER SB WHERE BRC.ITEMCODE=ITM.ITEMCODE AND BRC.BARCODE=STK.urunBarkod AND SB.ID=STK.subeID AND SB.subeAdi='" + subeAdi + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        [WebMethod]
        public System.Data.DataSet stokHareketiAra(string barcode)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            string cs = GetCs();
            SqlConnection conn = new SqlConnection(cs);
            string query = "select DISTINCT  BRC.BARCODE AS BARKOD,ITM.ITEMNAME AS [ÜRÜN ADI],ITM.COLOR AS RENK,ITM.SHAPE AS [ŞEKİL],ITM.FABRIC AS DOKU,BRC.STATUS_  AS DURUM,STKH.cikisSube AS [ÇIKIŞ ŞUBE], STKH.cikisTarih AS [ÇIKIŞ TARİHİ] ,STKH.varisSube AS [VARIŞ ŞUBE],STKH.varisTarih AS [VARIŞ TARİHİ],STKH.durum AS DURUM"
            + " from BARCODES BRC,ITEMS ITM,STOK_HAREKETLERI STKH WHERE BRC.ITEMCODE=ITM.ITEMCODE AND BRC.BARCODE=STKH.urunBarkod AND BRC.BARCODE='" + barcode + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        [WebMethod]
        public System.Data.DataSet faturaAra(string faturaKodu)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            string cs = GetCs();
            SqlConnection conn = new SqlConnection(cs);
            string query = "select  MSR.musteriKodu AS [MÜŞTERİ KODU], FTR.faturaKodu AS [FATURA KODU],MSR.musteriAdSoyad AS [AD SOYAD],FTR.urunFiyat AS [ÜRÜN FİYAT],FTR.urunMiktar AS [URUN MİKTARI],FTR.toplamFiyat AS [TOPLAM FİYAT]"
                +" from MUSTERILER MSR,FATURA FTR"
                + " WHERE MSR.ID=FTR.musteriID AND FTR.faturaKodu='" + faturaKodu + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        [WebMethod]
        public System.Data.DataSet ayrintiliFaturaAra(string faturaKodu)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            string cs = GetCs();
            SqlConnection conn = new SqlConnection(cs);
            string query = "select  MSR.musteriKodu AS [MÜŞTERİ KODU], FTR.faturaKodu AS [FATURA KODU],MSR.musteriAdSoyad AS [AD SOYAD],FTR.urunFiyat AS [ÜRÜN FİYAT],"
            +" FTR.urunMiktar AS [URUN MİKTARI],FTR.toplamFiyat AS [TOPLAM FİYAT],FTR.faturaTarihi AS [FATURA TARİHİ],SB.subeAdi AS [ŞUBE ADI],SB.adres AS [ADRES]"
            +" from MUSTERILER MSR,FATURA FTR,SUBELER SB"
	        +" WHERE MSR.ID=FTR.musteriID  AND SB.ID=FTR.subeID"
	        +" AND FTR.faturaKodu='" + faturaKodu + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        [WebMethod]
        public System.Data.DataSet siparisEkle(string urunBarkodu,int siparisAdeti , int subeID)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            string cs = GetCs();
            SqlConnection conn = new SqlConnection(cs);
            string query = "begin tran "
              + " INSERT INTO SIPARIS(siparisKodu, siparisUrunBarkod, siparisAdeti, siparisSubeID, siparisDurumu, siparisTarihi) VALUES (abs(checksum(NewId()) % 10000),'" + urunBarkodu + "','" + siparisAdeti + "','" + subeID + "','Hazırlanıyor',GETDATE()) "
              +" commit tran";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        [WebMethod]
        public DataSet siparisListele()
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            string cs = GetCs();
            SqlConnection conn = new SqlConnection(GetCs());
            string query = "select SP.siparisKodu AS [SİPARİŞ KODU],SP.siparisUrunBarkod AS [ÜRÜN BARKODU],SP.siparisAdeti AS [SİPARİŞ ADETİ],SP.siparisDurumu AS [SİPARİŞ DURUMU], "
            +" SP.siparisTarihi AS [SİPARİŞ TARİHİ],SB.subeAdi AS [ŞUBE ADI]"
            +" from SIPARIS SP,SUBELER SB"
            + " WHERE SB.ID=SP.siparisSubeID";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;

        }
        [WebMethod]
        public System.Data.DataSet satisYap(string urunBarkodu, int musteriID, int subeID,int urunMiktari)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            string cs = GetCs();
            SqlConnection conn = new SqlConnection(cs);
            string query = "BEGIN TRAN "
            + " declare @urunFiyat float, "
            + " @barcode varchar(25)='" + urunBarkodu + "'" 

            + " select "
            + " @urunFiyat =(select DISTINCT ITM.PRICE from ITEMS ITM, BARCODES BR where ITM.ITEMCODE=BR.ITEMCODE and BARCODE=@barcode) "
            + " INSERT INTO FATURA(faturaKodu, musteriID, subeID, urunBarkod, urunFiyat, urunMiktar, toplamFiyat, faturaTarihi ) VALUES (abs(checksum(NewId()) % 10000),'" + musteriID + "','" + subeID + "',@barcode,@urunFiyat,'" + urunMiktari + "',@urunFiyat*'" + urunMiktari + "',GETDATE()) "
            + " commit tran";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        [WebMethod]
        public System.Data.DataSet faturaGoster()
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            string cs = GetCs();
            SqlConnection conn = new SqlConnection(cs);
            string query = "BEGIN TRAN "
			+" DECLARE @faturaID int; "
			+" SET @faturaID=(SELECT MAX(FATURA.id) FROM FATURA) "
			+" SELECT DISTINCT FTR.faturaKodu AS [FATURA KODU],MSR.musteriAdSoyad AS [MÜŞTERİ AD SOYAD],FTR.urunBarkod AS [ÜRÜN BARKODU],FTR.urunFiyat AS [BİRİM FİYAT], "
			+" FTR.urunMiktar AS [ÜRÜN MİKTARI],FTR.toplamFiyat AS [TOPLAM FİYAT], FTR.faturaTarihi AS [FATURA TARİHİ] "
			+" FROM FATURA FTR,MUSTERILER MSR WHERE MSR.ID=FTR.musteriID AND @faturaID=FTR.id "
			  +" COMMIT TRAN";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
       
    }
}