
CREATE FUNCTION [MixingStation].[GetSyringeInformation]
(
	@Origin varchar(100),
	@OriginId int
)

Returns @SyringeInformation Table
(
	CODVIAADM varchar(100),
	AdministrationName varchar(100)
) 

As
Begin	
			
	IF @Origin is not null  and @OriginId is not null
		begin
			If @Origin = 'HCPRESCRA'
				begin
				insert into @SyringeInformation
			    SELECT  h.CODVIAADM, ar.DESVIAADM as AdministrationName from HCPRESCRA h
				left join HCVIAADMI ar on ar.CODVIAADM = h.CODVIAADM
				where h.Id = @OriginId
				
			end
			else if @Origin ='HCINFLIQA'
				begin 
				insert into @SyringeInformation
				SELECT VIAADMDIL as CODVIAADM, ar.DESVIAADM as AdministrationName from HCINFLIQA h
				left join dbo.HCINFLIQD  hc on hc.CODCONCEC = H.CODCONCEC
				left join HCVIAADMI ar on ar.CODVIAADM = hc.VIAADMDIL 
				where h.CONSECUTI = @OriginId
			end	
			else if @Origin ='HCORDMEDICAM'
				begin 
				insert into @SyringeInformation
				SELECT h.CODVIAADM as CODVIAADM, ar.DESVIAADM as AdministrationName  from EHR.HCORDMEDICAM h
				left join HCVIAADMI ar on ar.CODVIAADM = h.CODVIAADM 
				where h.Id = @OriginId
			end	
	END
	ELSE
		BEGIN
		INSERT into @SyringeInformation
		VALUES(NULL,NULL)
	END
	Return
End