CREATE TABLE [dbo].[HCMUERECO] (
    [AUTO]       INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AUTOLABOR]  INT            NOT NULL,
    [NUMUESTRA]  INT            NOT NULL,
    [ESTMUESTRA] INT            NOT NULL,
    [FECRECMUE]  DATETIME       NULL,
    [USURECMUE]  CHAR (20)      NULL,
    [OBSRVAMUE]  VARCHAR (300)  NULL,
    [RESULTADO]  BIT            NULL,
    [AUTODOCUM]  INT            NULL,
    [EXTDOCUME]  VARCHAR (10)   NULL,
    [INTERPRET]  VARCHAR (4000) NULL,
    CONSTRAINT [PK_HCMUERECO] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Interpretacion de la Muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'INTERPRET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Extension Documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'EXTDOCUME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Documento Adjunto ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'AUTODOCUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica si la muestra se adjunto un documento como resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para especificar cuando se ha anulado una muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'OBSRVAMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Recolecta Muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'USURECMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Recoleccion Muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'FECRECMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Muetra 0:No Recolectada;1:Recolectada;2:Anulada;3:Resultado;4:Interpretada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'ESTMUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la Muestra Recolectada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'NUMUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico del Laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'AUTO';

