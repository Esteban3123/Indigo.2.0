CREATE TABLE [Report].[Table_CENSO] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [DateInsert]          DATETIME      NOT NULL,
    [CodUnidadNegocio]    VARCHAR (15)  NOT NULL,
    [NomUnidadNegocio]    VARCHAR (50)  NOT NULL,
    [CodCentroAtencion]   VARCHAR (10)  NOT NULL,
    [NomCentroAtencion]   VARCHAR (100) NOT NULL,
    [CodUniadFuncional]   VARCHAR (10)  NOT NULL,
    [NomUnidadFuncional]  VARCHAR (60)  NOT NULL,
    [TipoUnidadFuncional] VARCHAR (50)  NOT NULL,
    [Cama]                VARCHAR (50)  NOT NULL,
    [EstadoCama]          VARCHAR (25)  NOT NULL,
    [EstadoReserva]       VARCHAR (25)  NULL,
    [NroIngreso]          VARCHAR (10)  NULL,
    [FecIniciaEstancia]   DATETIME      NULL,
    [DiasEstancia]        SMALLINT      NULL,
    [TipoEstancia]        VARCHAR (40)  NULL,
    [FecAltaMedica]       DATETIME      NULL,
    [NroDocumento]        VARCHAR (25)  NULL,
    [TipoDocumento]       CHAR (2)      NULL,
    [NombrePaciente]      VARCHAR (250) NULL,
    [Edad]                SMALLINT      NULL,
    [CodEPS]              VARCHAR (20)  NULL,
    [NomEPS]              VARCHAR (300) NULL,
    [CodGrpAtencion]      VARCHAR (20)  NULL,
    [NomGrpAtencion]      VARCHAR (100) NULL,
    [Regimen]             VARCHAR (30)  NULL,
    [CodDiagnostico]      VARCHAR (6)   NULL,
    [NomDiagnostico]      VARCHAR (350) NULL,
    [CodMedico]           VARCHAR (20)  NULL,
    [NomMedico]           VARCHAR (60)  NULL,
    [Especialidad]        VARCHAR (60)  NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_Table_CENSO]
    ON [Report].[Table_CENSO]([Id] ASC);

