
CREATE Procedure [dbo].[SPMOV_ListarInformacionPacientesResumen]
(
 @Paciente varchar(25),
 @Ingreso varchar(50)
)
AS
Select  RTRIM(B.IPNOMCOMP) as 'Paciente',
        B.IPFECNACI as 'FechaNacimiento',
		SUBSTRING (dbo.ObtenerFechaFormateada(B.IPFECNACI) ,0 , 11)  as 'FechaFormateada',
		EdadFormateada = '',
		RTRIM(B.CODIGONIT) as 'Identificacion',
		CASE
			when B.IPSEXOPAC = 1 then 'Masculino'
			when B.IPSEXOPAC = 2 then 'Femenino'
			else ' '
		END as 'Sexo',
		CASE
			when B.IPGRUPSAN is null then 'No registra Grupo Sanguineo'
			else B.IPGRUPSAN
		END as 'GrupoSanguineo',
		CASE
			when B.IPRHSANGR is null then 'No registra Rh'
			else B.IPRHSANGR
		END as 'RhSanguineo',
		RTRIM(B.IPDIRECCI) as 'Direccion',
		RTRIM(B.IPTELEFON) as 'Fijo',
		RTRIM(B.IPTELMOVI) as 'Movil',
		RTRIM(C.NOMDIAGNO) as 'Diagnostico'
		-- Alertas por Medicamentos //PARTE DE ALERGIAS A MEDICAMENTOS
		--RTRIM(E.DESPRODUC) AS 'Producto',
		--RTRIM(D.MOTSUSMED) AS 'MotivoSuspencion',
		--RTRIM(D.CODPRODUC) AS 'CodigoProducto',
		--RTRIM(d.NUMEFOLIO) AS 'Folio',
		--Escalas de Riesgo //PARTE DE ESCALAS DE RIESGO
		--CASE 
		--	when RTRIM(ESCADOWNT) = 1 then 'Si'
		--	when RTRIM(ESCADOWNT) = 0 then 'No'
		--	when RTRIM(ESCADOWNT) is null then 'No'
		--end as 'DownTon',
		--CASE 
		--	when RTRIM(ESCABIERI) = 1 then 'Si'
		--	when RTRIM(ESCABIERI) = 0 then 'No'
		--	when RTRIM(ESCABIERI) is null then 'No'
		--end as 'Bieri',
		--CASE 
		--	when RTRIM(ESCARASS) = 1 then 'Si'
		--	when RTRIM(ESCARASS) = 0 then 'No'
		--	when RTRIM(ESCARASS) is null then 'No'
		--end as 'RASS',
		--CASE 
		--	when RTRIM(ESCNORPAC) = 1 then 'Si'
		--	when RTRIM(ESCNORPAC) = 0 then 'No'
		--	when RTRIM(ESCNORPAC) is null then 'No'
		--end as 'NorTon',
		--CASE 
		--	when RTRIM(ESCVASPAC) = 1 then 'Si'
		--	when RTRIM(ESCVASPAC) = 0 then 'No'
		--	when RTRIM(ESCVASPAC) is null then 'No'
		--end as 'VAS',
		--CASE 
		--	when RTRIM(ESCAPAPAC) = 1 then 'Si'
		--	when RTRIM(ESCAPAPAC) = 0 then 'No'
		--	when RTRIM(ESCAPAPAC) is null then 'No'
		--end as 'Apache'
		-- PARTE QUE REPRESENTA LOS ANTECEDENTES
		--F.ANTMEDPAC as 'Medicos',
		--F.ANTQUIPAC as 'Quirurgicos',	
		--F.ANTTRAPAC as 'Transfucionales',
		--F.ANTINMPAC as 'Inmunologicos',
		--F.ANTALEPAC as 'Alergicos',
		--F.ANTTRUPAC as 'Traumaticos',	
		--F.ANTPSIPAC as 'Psicologicos&Psiquiatricos',
		--F.ANTFARPAC as 'Farmacologicos',
		--F.ANTFAMPAC as 'Familiares',
		--F.ANTTOXPAC as 'Toxicos',
		--F.ANTOTRPAC as 'Otros'
from ADINGRESO A
inner join INPACIENT B on B.IPCODPACI = A.IPCODPACI
inner join INDIAGNOS C on C.CODDIAGNO =  (SELECT TOP 1 CODDIAGNO FROM INDIAGNOP WHERE IPCODPACI = B.IPCODPACI AND NUMINGRES = A.NUMINGRES AND CODDIAPRI = 1)
--left JOIN HCMEDRIES D ON D.IPCODPACI = A.IPCODPACI AND D.NUMINGRES = A.NUMINGRES
--left join IHLISTPRO E on E.CODPRODUC = D.CODPRODUC
--left join HCANTPACH F on F.IPCODPACI = B.IPCODPACI
where A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso