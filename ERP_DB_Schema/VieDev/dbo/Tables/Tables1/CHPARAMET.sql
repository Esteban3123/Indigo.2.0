CREATE TABLE [dbo].[CHPARAMET] (
    [CODCENATE]                                CHAR (10)    NOT NULL,
    [REQAPRSOL]                                BIT          NOT NULL,
    [PERSOLRES]                                BIT          NOT NULL,
    [HORMINSOL]                                TINYINT      NOT NULL,
    [PERHOSSIN]                                BIT          NOT NULL,
    [NUMDIAMAX]                                TINYINT      NOT NULL,
    [NUMHORMIN]                                TINYINT      NOT NULL,
    [FECPARINI]                                DATETIME     NOT NULL,
    [TIEMTABLE]                                INT          NOT NULL,
    [HORRESALE]                                TINYINT      NULL,
    [EXICOINFC]                                BIT          NOT NULL,
    [INDAUDFOR]                                NUMERIC (18) NOT NULL,
    [ACTINTERF]                                CHAR (1)     NOT NULL,
    [GENHORACORTE]                             TIME (0)     CONSTRAINT [DF__CHPARAMET__GENHO__16A44564] DEFAULT ('23:59:59') NOT NULL,
    [DASHLIMPIEZA]                             BIT          NULL,
    [BreakfastStartTime]                       TIME (0)     NULL,
    [BreakfastEndTime]                         TIME (0)     NULL,
    [LunchStartTime]                           TIME (0)     NULL,
    [LunchEndTime]                             TIME (0)     NULL,
    [DinnerStartTime]                          TIME (0)     NULL,
    [DinnerEndTime]                            TIME (0)     NULL,
    [MorningSupplementStartTime]               TIME (0)     NULL,
    [MorningSupplementEndTime]                 TIME (0)     NULL,
    [AfternoonSupplementStartTime]             TIME (0)     NULL,
    [AfternoonSupplementEndTime]               TIME (0)     NULL,
    [OtherMealStartTime]                       TIME (0)     NULL,
    [OtherMealEndTime]                         TIME (0)     NULL,
    [SaveDischargeWithAllLegalizedMedications] BIT          CONSTRAINT [DF__CHPARAMET__SaveD__45D06EB0] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_CHPARAMET] PRIMARY KEY CLUSTERED ([CODCENATE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guardar Descarga ConTodos LosMedicamentos Legalizados  1 = true    o = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'SaveDischargeWithAllLegalizedMedications';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ultima hora del desayuno.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'BreakfastEndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Inicial del desayuno  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'BreakfastStartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si se activa la interfaz de homologacion de camas para validar en hospitalizacion   0: No  1: Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'ACTINTERF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '01-06-2024 Apartir de la fecha, este campo se marca obsoleto por el PBI: 17646 en Azure Devops.       Exigir Consentimiento Informado Cirugia   1:Si;  0:No;    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'EXICOINFC';

