CREATE TABLE [dbo].[HCALEMOTA] (
    [CODMOTANU] CHAR (2)  NOT NULL,
    [DESMOTANU] CHAR (80) NOT NULL,
    [TIPSERIPS] INT       NULL,
    CONSTRAINT [PK_HCALEMOTA] PRIMARY KEY CLUSTERED ([CODMOTANU] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Servicio  1: Laboratorios  2: Patologias  3: Imagenes Diagnosticas  4: Procedimeintos no Qx  5: Procedimientos Qx  6: Interconsultas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEMOTA', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Motivo de Anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEMOTA', @level2type = N'COLUMN', @level2name = N'DESMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Motivo de Anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEMOTA', @level2type = N'COLUMN', @level2name = N'CODMOTANU';

