CREATE TABLE [dbo].[HCRECEPINTER] (
    [ID]           INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCREFRECEPID] INT         NOT NULL,
    [CORREO]       NCHAR (100) NOT NULL,
    [ESTADO]       INT         NOT NULL,
    [FECHAENV]     DATETIME    NULL,
    CONSTRAINT [PK_HCRECEPINTER] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRECEPINTER_HCREFRECEP] FOREIGN KEY ([HCREFRECEPID]) REFERENCES [dbo].[HCREFRECEP] ([ID])
);


GO
ALTER TABLE [dbo].[HCRECEPINTER] NOCHECK CONSTRAINT [FK_HCRECEPINTER_HCREFRECEP];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y hora de envío del correo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPINTER', @level2type = N'COLUMN', @level2name = N'FECHAENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del envío del correo:  1 ->Correo No enviado  2 -> Correo enviado   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPINTER', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Correo electrónico - Email', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPINTER', @level2type = N'COLUMN', @level2name = N'CORREO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id que relaciona el registro de la cabecera: tabla HCREFRECEP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPINTER', @level2type = N'COLUMN', @level2name = N'HCREFRECEPID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPINTER', @level2type = N'COLUMN', @level2name = N'ID';

