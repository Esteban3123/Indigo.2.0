CREATE TABLE [PathologyALULA].[INTEGRATIONCONTROL] (
    [id]                   INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [idpathology]          INT         NOT NULL,
    [CUPS]                 CHAR (20)   NOT NULL,
    [ordertype]            VARCHAR (3) NOT NULL,
    [registrationdate]     DATETIME    NOT NULL,
    [coderegistrationuser] CHAR (20)   NOT NULL,
    CONSTRAINT [PK_PATHOLOGYINTEGRATION] PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo usuario que registra interfaz', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL', @level2type = N'COLUMN', @level2name = N'coderegistrationuser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha creacion registro', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL', @level2type = N'COLUMN', @level2name = N'registrationdate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Orden:  INT - Intrahospitalario  AMB - Ambulatorio', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL', @level2type = N'COLUMN', @level2name = N'ordertype';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo CUPS enviado', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL', @level2type = N'COLUMN', @level2name = N'CUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de orden de patologia puede ser de la tabla (HCORDPATO - AMBORPATO)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL', @level2type = N'COLUMN', @level2name = N'idpathology';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico tabla', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTEGRATIONCONTROL', @level2type = N'COLUMN', @level2name = N'id';

