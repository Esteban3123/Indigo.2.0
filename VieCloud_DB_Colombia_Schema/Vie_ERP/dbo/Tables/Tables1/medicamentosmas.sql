CREATE TABLE [dbo].[medicamentosmas] (
    [ID_COMPANY]                   VARCHAR (9)     NULL,
    [TIPO]                         VARCHAR (12)    NOT NULL,
    [Cantidad formulada]           INT             NULL,
    [Codigo Medicamento]           CHAR (20)       NOT NULL,
    [Descripcion Medicamento]      CHAR (255)      NOT NULL,
    [Tipo Formulacion]             VARCHAR (26)    NULL,
    [Peso]                         NUMERIC (18, 2) NULL,
    [Codigo Unidad Peso]           CHAR (20)       NULL,
    [unidad_peso]                  VARCHAR (100)   NULL,
    [volumen]                      NUMERIC (18, 2) NULL,
    [Codigo Unidad volumne]        CHAR (20)       NULL,
    [unidad_volumen]               VARCHAR (100)   NULL,
    [Codigo Unidad Administracion] CHAR (20)       NULL,
    [unidad_admin]                 VARCHAR (100)   NULL,
    [Codigo Forma]                 VARCHAR (20)    NOT NULL,
    [Nombre Forma]                 CHAR (100)      NOT NULL,
    [Control]                      VARCHAR (2)     NOT NULL,
    [PBS]                          VARCHAR (2)     NOT NULL,
    [CANTIDAD]                     INT             NOT NULL,
    [ULT_ACTUAL]                   DATETIME        NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de catálogo de medicamentos que almacena información técnica y de formulación de cada ítem, incluyendo descripción, forma farmacéutica, unidades de peso, volumen y administración, así como cantidad formulada. Registra atributos regulatorios como si el medicamento pertenece al Plan de Beneficios en Salud (PBS) y si es de control especial. Se asocia a una compañía y mantiene fecha de última actualización.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'medicamentosmas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'medicamentosmas';
GO
