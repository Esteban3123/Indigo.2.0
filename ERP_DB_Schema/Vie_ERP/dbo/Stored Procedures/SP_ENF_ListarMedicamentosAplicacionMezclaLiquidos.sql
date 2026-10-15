
CREATE PROCEDURE [dbo].[SP_ENF_ListarMedicamentosAplicacionMezclaLiquidos]
(
@CodigoPaciente varchar(25),
@CodigoIngreso AS CHAR(10),
@CodigoCentroAtencion AS CHAR(10),
@CodigoUnidadFuncional AS CHAR(10),
@ParametroRegistraMedicamentosSinCantidadDisponibles AS BIT,
@ListarInformacionCabecera AS INTEGER,
@IDCabeceraHojaMezclas INT,
@IDCabeceraOrdenMezcla INT


)
AS
BEGIN
	SET NOCOUNT ON;
  
 IF @ListarInformacionCabecera = 1 --listar informacion cabecera
 BEGIN

	 SELECT 
			C.CONSECUTI AS 'IDCabeceraHojaMezclas' , C.IDHCINFLIQC AS 'IDCabeceraOrdenMezcla',
			C.IPCODPACI AS 'Identificacion',C.NUMINGRES AS 'Ingreso', C.CODCENATE AS 'Codigo Centro', C.UFUCODIGO AS 'Codigo Unidad Funcional',
			RTRIM(C.NOMMEZCLA) AS 'Nombre Mezcla',RTRIM(C.INDAPLMED) AS 'Dosis', RTRIM(I.NOMMEDICO) AS 'Medico Prescrible',C.FECPROGRAMACIONAPL AS 'Fecha Programacion Para Aplicacion',
			CASE C.TIPMEZLIQ when '1' then 'Mezcla Continua' when '2' then 'Liquido' when '3' then 'Mezcla Frecuencia' when '4' then 'Mezcla Magistral' end as 'Tipo Mezcla',
			CASE c.METAPLMED when '1' then 'Bolo Mezcla' when '2' then 'Infusion Mezcla' when '3' then 'Bolo Medicamento Mezcla' WHEN 4 THEN 'Infusion Liquidos' WHEN '5' THEN 'Bolo Medicamento Liquido' end as 'Solicitud',c.METAPLMED,
			CONCAT(c.INDAPLMED,' - ', c.INDAPLMEDADICIONALES) AS 'Instrucciones',
			C.DOSISUNICA, C.DOSISAPLICACION, C.CODUNIMEDIAPLICACION, RTRIM(x.CODUNIMED) +'-'+ RTRIM(x.DESUNIMED) AS 'DESUNIMED' , C.DURINFUSION, C.UNIDADINFUSION, C.DURFRECUENCIA, C.UNIDADFRECUENCIA,C.DURACIDOS, C.VALDURFIJ, C.UNIDURFIJ --campos de la mezcla magistral - Mezcla Frecuencia
		
		FROM HCHOJMEZC  C
			INNER JOIN dbo.INPROFSAL I WITH (NOLOCK) ON C.CODPROSAL = I.CODPROSAL
			LEFT JOIN dbo.INUNIMEDI x WITH (NOLOCK) ON c.CODUNIMEDIAPLICACION = x.CODUNIMED
		WHERE C.CONSECUTI = @IDCabeceraHojaMezclas
 end
 ELSE IF @ListarInformacionCabecera = 2 --Listar medicamentos detalle
 BEGIN
	
	IF @ParametroRegistraMedicamentosSinCantidadDisponibles = 1 BEGIN
    
			SELECT distinct RTRIM(A.CODPRODUC) AS Codigo, RTRIM(A.DESPRODUC) AS Medicamento,v.CANTIUTIL AS 'Cantidad', SUM(CAST(COALESCE(NULLIF(B.QUANTITY,0),0) AS numeric )) AS Fisico,CODDCIMED as 'DCI' 
			FROM dbo.IHLISTPRO A 
			INNER JOIN HCHOJMEZD v WITH (NOLOCK) ON a.CODPRODUC = v.CODPRODUC AND v.CONSECUTI =  @IDCabeceraHojaMezclas
			INNER JOIN Inventory.ATC AS C WITH (NOLOCK) ON A.CODPRODUC = C.Code 
			INNER JOIN Inventory.InventoryProduct AS P WITH (NOLOCK) ON C.Id = P.ATCId 
			LEFT OUTER JOIN Inventory.PhysicalInventory AS B WITH (NOLOCK) ON B.ProductId = P.id AND B.WarehouseId IN (select Id from Inventory.Warehouse
			WHERE CodeCenterAttention = @CodigoCentroAtencion AND v.CONSECUTI =  @IDCabeceraHojaMezclas
			UNION ALL 
			SELECT Id from Inventory.Warehouse where CodeCenterAttention IS NULL) 
			INNER JOIN Inventory.Warehouse AS E WITH (NOLOCK) ON B.WarehouseId = E.Id 
			WHERE A.TIPPRODUC IN ('1','3') AND A.PROESTADO = 1 GROUP BY A.CODPRODUC,A.DESPRODUC,v.CANTIUTIL,CODDCIMED 

	END
    ELSE  begin
			
			/*SELECT distinct RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,v.CANTIUTIL AS 'Cantidad',A.CANACTPRO AS Fisico,B.CODDCIMED as 'DCI'
			FROM dbo.HCFISIPRO A
			INNER JOIN dbo.IHLISTPRO B WITH (NOLOCK) ON A.CODPRODUC=B.CODPRODUC 
			INNER JOIN HCHOJMEZD v WITH (NOLOCK) ON a.CODPRODUC = v.CODPRODUC and v.CONSECUTI =  @IDCabeceraHojaMezclas
			INNER JOIN HCHOJMEZC c WITH (NOLOCK) ON v.CONSECUTI = c.CONSECUTI
			WHERE A.IPCODPACI= @CodigoPaciente AND A.NUMINGRES=@CodigoIngreso AND A.CODCENATE=@CodigoCentroAtencion AND A.UFUCODIGO=@CodigoUnidadFuncional 
				  AND B.TIPPRODUC IN ('1','3') AND PROESTADO='1' and v.CONSECUTI =  @IDCabeceraHojaMezclas*/

			SELECT distinct RTRIM(B.CODPRODUC) AS Codigo, RTRIM(B.DESPRODUC) AS Medicamento,v.CANTIUTIL AS 'Cantidad',A.CANACTPRO AS Fisico,B.CODDCIMED as 'DCI'
			FROM 
		     HCHOJMEZC c WITH (NOLOCK) 
			INNER JOIN HCHOJMEZD v WITH (NOLOCK) ON v.CONSECUTI = c.CONSECUTI  and v.CONSECUTI =  @IDCabeceraHojaMezclas
			INNER JOIN dbo.IHLISTPRO B WITH (NOLOCK) ON b.CODPRODUC = v.CODPRODUC
			LEFT JOIN HCFISIPRO  A on A.CODPRODUC = V.CODPRODUC AND a.NUMINGRES = c.NUMINGRES and a.IPCODPACI = c.IPCODPACI 
			WHERE A.IPCODPACI= @CodigoPaciente AND A.NUMINGRES=@CodigoIngreso AND A.CODCENATE=@CodigoCentroAtencion  AND A.UFUCODIGO=@CodigoUnidadFuncional
				  AND B.TIPPRODUC IN ('1','3') AND B.PROESTADO='1' and v.CONSECUTI =  @IDCabeceraHojaMezclas
	
	END
  END
 end