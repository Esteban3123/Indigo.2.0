CREATE TABLE [dbo].[INDOCUMEN] (
    [CODDOCUME] TINYINT   NOT NULL,
    [NUMDOCUME] CHAR (40) NOT NULL,
    [CODUSUARI] CHAR (20) NOT NULL,
    CONSTRAINT [PK_INDOCUMEN] PRIMARY KEY CLUSTERED ([CODDOCUME] ASC, [NUMDOCUME] ASC, [CODUSUARI] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Documento  Triage = 1  Camas = 2  Ingresos = 3  Pacientes = 4  Enfermeria_ValoracionNeurologica = 5  Enfermeria_BalanceLiquidos = 6  Enfermeria_SignosVitales = 7  Enfermeria_Notas = 8  Enfermeria_Pendientes = 9  Enfermeria_HistoriaFinal = 10  Enfermeria_HistoriaInicial = 11  Enfermeria_HojaMedicamentos = 15  Enfermeria_HojaMezclas = 16  Enfermeria_HojaInsumos = 17  Enfermeria_RegistroProcedimientos = 18  Enfermeria_Monitoriarespiratoria = 19  Terapia_Notas = 13  Terapia_ProcedimientosInsumos = 14  Terapia_Monitoriarespiratoria = 20  Terapia_BalanceAcidoBase = 21  Inventario_DespachoFarmacia = 30  Devolutivo_Enfermeria = 32  Egreso_GetionHospitalaria = 33  Hoja_Gasto_QX = 34  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDOCUMEN', @level2type = N'COLUMN', @level2name = N'CODDOCUME';

