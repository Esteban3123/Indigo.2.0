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

