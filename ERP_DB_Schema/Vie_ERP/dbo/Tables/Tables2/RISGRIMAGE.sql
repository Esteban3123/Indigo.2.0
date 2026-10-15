CREATE TABLE [dbo].[RISGRIMAGE] (
    [ID]                          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]                      VARCHAR (50)  NOT NULL,
    [NOMBRE]                      VARCHAR (100) NOT NULL,
    [ESTADO]                      BIT           CONSTRAINT [DF_RISGRIMAGE_ESTADO] DEFAULT ((1)) NOT NULL,
    [ABREVIACION]                 VARCHAR (50)  NULL,
    [DefineRoomIntrahospitalario] BIT           CONSTRAINT [DF_RISGRIMAGE_DefineRoomIntrahospitalario] DEFAULT ((0)) NULL,
    [DefineRoutingToTheInterface] BIT           NOT NULL,
    CONSTRAINT [PK_RISGRIMAGE] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Definir sala ámbito intrahospitalario ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'DefineRoomIntrahospitalario';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la descripción de la abreviación imagenologias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'ABREVIACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 0-->Inactivo, 1-->Activo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion o nombre de Grupo Imagenologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Grupo de Imagenologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'ID';

