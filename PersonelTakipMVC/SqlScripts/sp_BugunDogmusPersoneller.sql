CREATE PROCEDURE sp_EnYasliPersonelGetir
AS
BEGIN
	SELECT TOP 1 *
	FROM Personeller
	ORDER BY DogumTarihi ASC
END