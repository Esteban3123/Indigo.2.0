CREATE TABLE [dbo].[ODONTOMOTIVOSNOCUMPLITRATA] (
    [ID]            INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]        VARCHAR (3)  NOT NULL,
    [NOMBRE]        VARCHAR (50) NOT NULL,
    [ESTADO]        BIT          NOT NULL,
    [FECHACREACION] DATETIME     NOT NULL,
    [USUARIOCREA]   CHAR (20)    NOT NULL,
    [FECHAMODIFI]   DATETIME     NULL,
    [USUMODIFICA]   CHAR (20)    NULL,
    CONSTRAINT [PK_ODONTOMOTIVOSNOCUMPLITRATA] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el usuario quien lo modifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'USUMODIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'FECHAMODIFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el usuario quien  lo crea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'USUARIOCREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el estado  1 =Activo     0 = Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el nombre o la descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el código Motivos No Tratamientos Odontologicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOMOTIVOSNOCUMPLITRATA', @level2type = N'COLUMN', @level2name = N'ID';

