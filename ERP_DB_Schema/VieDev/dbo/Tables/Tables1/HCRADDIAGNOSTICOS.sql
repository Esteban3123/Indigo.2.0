CREATE TABLE [dbo].[HCRADDIAGNOSTICOS] (
    [ID]              INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADESQUEMAS] INT      NOT NULL,
    [CODDIAGNO]       CHAR (4) NOT NULL,
    CONSTRAINT [PK_HCRADDIAGNOSTICOS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el ID de  radicacion de esquemas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'IDHCRADESQUEMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'ID';

