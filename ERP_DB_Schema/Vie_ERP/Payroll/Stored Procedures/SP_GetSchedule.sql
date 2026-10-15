	-- =============================================
	-- Author:		<Juan Montealegre>
	-- Create date: <27-07-2014>
	-- Description:	<Listar el cuadro de turno>
	-- =============================================
	CREATE PROCEDURE [Payroll].[SP_GetSchedule]
	@UnidadFuncional int,
	@Periodo varchar(7)
	AS
	BEGIN
		-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;

	SELECT 

	a.EmployeeId ,
	c.Name 'NombreEmpleado' ,
		
			----Letra turno asignado
			dbo.[GetLetter](D01) 'D1',
			dbo.[GetLetter](D02) 'D2',
			dbo.[GetLetter](D03) 'D3',
			dbo.[GetLetter](D04) 'D4',
			dbo.[GetLetter](D05) 'D5',
			dbo.[GetLetter](D06) 'D6',
			dbo.[GetLetter](D07) 'D7',
			dbo.[GetLetter](D08) 'D8',
			dbo.[GetLetter](D09) 'D9',
			dbo.[GetLetter](D10) 'D10',
			dbo.[GetLetter](D11) 'D11',
			dbo.[GetLetter](D12) 'D12',
			dbo.[GetLetter](D13) 'D13',
			dbo.[GetLetter](D14) 'D14',
			dbo.[GetLetter](D15) 'D15',
			dbo.[GetLetter](D16) 'D16',
			dbo.[GetLetter](D17) 'D17',
			dbo.[GetLetter](D18) 'D18',
			dbo.[GetLetter](D19) 'D19',
			dbo.[GetLetter](D20) 'D20',
			dbo.[GetLetter](D21) 'D21',
			dbo.[GetLetter](D22) 'D22',
			dbo.[GetLetter](D23) 'D23',
			dbo.[GetLetter](D24) 'D24',
			dbo.[GetLetter](D25) 'D25',
			dbo.[GetLetter](D26) 'D26',
			dbo.[GetLetter](D27) 'D27',
			dbo.[GetLetter](D28) 'D28',
			dbo.[GetLetter](D29) 'D29',
			dbo.[GetLetter](D30) 'D30',
			dbo.[GetLetter](D30) 'D31',
			A.TotalHour 'TotalHoras'

			 FROM 
			 payroll.SCHEDULE A 
			 INNER JOIN
			 Payroll.Employee B ON A.EmployeeId = B.Id 
			 INNER JOIN
			 Common.ThirdParty C ON B.ThirdPartyId = C.Id 

		WHERE
			A.FunctionalUnitId =@unidadfuncional and A.Period = @periodo

			-- select * from [Payroll].[VistaLetra2]

	END