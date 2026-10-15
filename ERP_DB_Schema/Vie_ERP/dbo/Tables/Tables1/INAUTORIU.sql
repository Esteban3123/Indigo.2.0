CREATE TABLE [dbo].[INAUTORIU] (
    [PKTIPOCON] INT          NOT NULL,
    [CODUSUARI] CHAR (20)    NOT NULL,
    [PKVALORCO] CHAR (20)    NOT NULL,
    [INDAUDFOR] NUMERIC (18) NULL,
    CONSTRAINT [PK_INAUTORIU] PRIMARY KEY CLUSTERED ([PKTIPOCON] ASC, [CODUSUARI] ASC, [PKVALORCO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIU', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tabla de Autorizaciones - Indigo Crystal.Net', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIU', @level2type = N'COLUMN', @level2name = N'PKVALORCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIU', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo Reservado:  1: Centros de Atencion  2: Unidades Funcionales  3: Platillas de Edicion  4: Abrir mas de un Ingreso  5: Entidades  6: Agendamiento: Especialidades  7: Agendamiento: Salas  8: Dashboard: Sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIU', @level2type = N'COLUMN', @level2name = N'PKTIPOCON';

