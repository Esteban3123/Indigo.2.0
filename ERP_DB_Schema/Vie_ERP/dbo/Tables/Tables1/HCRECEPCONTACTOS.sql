CREATE TABLE [dbo].[HCRECEPCONTACTOS] (
    [ID]           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCREFRECDE] INT           NULL,
    [EMAIL]        VARCHAR (100) NULL,
    [CORRENVIADO]  BIT           NULL,
    CONSTRAINT [PK_HCRECEPCONTACTOS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Me Identifica si el Correo ya se ha enviado mediante el Servicio Windows', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPCONTACTOS', @level2type = N'COLUMN', @level2name = N'CORRENVIADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Correo Electronico al cual se le va enviar mensaje de Recepcion de Referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPCONTACTOS', @level2type = N'COLUMN', @level2name = N'EMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla  HCREFRECDE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPCONTACTOS', @level2type = N'COLUMN', @level2name = N'IDHCREFRECDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPCONTACTOS', @level2type = N'COLUMN', @level2name = N'ID';

