CREATE TABLE [dbo].[INDOCUMEN] (
    [CODDOCUME] TINYINT   NOT NULL,
    [NUMDOCUME] CHAR (40) NOT NULL,
    [CODUSUARI] CHAR (20) NOT NULL,
    CONSTRAINT [PK_INDOCUMEN] PRIMARY KEY CLUSTERED ([CODDOCUME] ASC, [NUMDOCUME] ASC, [CODUSUARI] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario, identificador único del profesional o personal que registra/crea el documento clínico o administrativo en el sistema. Tipo: CHAR(20), clave primaria compuesta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDOCUMEN', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDOCUMEN', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDOCUMEN', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del documento, identificador único secuencial o correlativo que identifica cada registro clínico, formulario, hoja de ingreso, atención o procedimiento. Tipo: CHAR(40), clave primaria compuesta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDOCUMEN', @level2type = N'COLUMN', @level2name = N'NUMDOCUME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDOCUMEN', @level2type = N'COLUMN', @level2name = N'NUMDOCUME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDOCUMEN', @level2type = N'COLUMN', @level2name = N'NUMDOCUME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de documento clínico-administrativo: Triage(1), Camas(2), Ingresos(3), Pacientes(4), Enfermeria-Valoración/Balance/Signos/Notas/Pendientes/Historias/Medicamentos/Mezclas/Insumos/Procedimientos/Monitoría(5-11,15-19), Terapia-Notas/Procedimientos/Monitoría/Balance(13-14,20-21), Farmacia(30), Devoluciones(32), Egreso(33), Gasto QX(34). Tipo: TINYINT, clave primaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDOCUMEN', @level2type = N'COLUMN', @level2name = N'CODDOCUME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Documento  Triage = 1  Camas = 2  Ingresos = 3  Pacientes = 4  Enfermeria_ValoracionNeurologica = 5  Enfermeria_BalanceLiquidos = 6  Enfermeria_SignosVitales = 7  Enfermeria_Notas = 8  Enfermeria_Pendientes = 9  Enfermeria_HistoriaFinal = 10  Enfermeria_HistoriaInicial = 11  Enfermeria_HojaMedicamentos = 15  Enfermeria_HojaMezclas = 16  Enfermeria_HojaInsumos = 17  Enfermeria_RegistroProcedimientos = 18  Enfermeria_Monitoriarespiratoria = 19  Terapia_Notas = 13  Terapia_ProcedimientosInsumos = 14  Terapia_Monitoriarespiratoria = 20  Terapia_BalanceAcidoBase = 21  Inventario_DespachoFarmacia = 30  Devolutivo_Enfermeria = 32  Egreso_GetionHospitalaria = 33  Hoja_Gasto_QX = 34  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDOCUMEN', @level2type = N'COLUMN', @level2name = N'CODDOCUME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDOCUMEN', @level2type = N'COLUMN', @level2name = N'CODDOCUME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documentos adjuntos o registros documentales del sistema, asociados a un tipo de documento, un número o contenido del documento y el usuario que lo gestiona.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDOCUMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDOCUMEN';
