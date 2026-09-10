CREATE TABLE [dbo].[HCENTREGATURNOREFEREN] (
    [ID]                      INT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCENTREGATURNOC]       INT        NOT NULL,
    [IDHCENTREGATURNOCPACIEN] INT        NOT NULL,
    [NUMEFOLIO]               NCHAR (10) NOT NULL,
    [IDHCREFCONP]             INT        NOT NULL,
    [ESTADO]                  INT        NOT NULL,
    CONSTRAINT [PK_HCENTREGATURNOREFER] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCENTREGATURNOREFER_HCENTREGATURNOC] FOREIGN KEY ([IDHCENTREGATURNOC]) REFERENCES [dbo].[HCENTREGATURNOC] ([ID]),
    CONSTRAINT [FK_HCENTREGATURNOREFER_HCENTREGATURNOCPACIEN] FOREIGN KEY ([IDHCENTREGATURNOCPACIEN]) REFERENCES [dbo].[HCENTREGATURNOCPACIEN] ([ID]),
    CONSTRAINT [FK_HCENTREGATURNOREFEREN_HCREFCONP] FOREIGN KEY ([IDHCREFCONP]) REFERENCES [dbo].[HCREFCONP] ([AUTO])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de gestión de referencia: 1=Solicitado, 2=Pendiente/Gestionando, 3=Aceptado con pediente de salida, 4=Suspendido, 5=Extramural (paciente ya egresó o referencia marcada extramural sin importar tipo de unidad funcional), 6=Solicitud con Pertinencia. INT, dominio controlado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado para saber como esta la gestion de la referencia   1->Solicitado.  2->Pendinte o Gestionando.  3->Aceptado con pediente de salida.  4->Suspendido.  5->Extramural/ ya salio. Se marca en 5 si la solicitud de referencia la marcan como tipo extramural sin importar si la Unidad funcional es de tipo consulta externa ó intrahospitalaria ´o Cuando hago una solicitud Intrahospitalaria y el paciente sale de la institución.  6->Solicitud con Pertinencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cabecera de referencia con profesional (tabla HCREFCONP). INT. Vincula detalle de referencia a datos maestros de la solicitud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN', @level2type = N'COLUMN', @level2name = N'IDHCREFCONP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla cabecera HCREFCONP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN', @level2type = N'COLUMN', @level2name = N'IDHCREFCONP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN', @level2type = N'COLUMN', @level2name = N'IDHCREFCONP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio de la referencia. NCHAR(10). Identificador textual único para búsqueda de solicitud de referencia o contrarreferencia en el expediente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cabecera turno-paciente (tabla HCENTREGATURNOCPACIEN). INT. Vincula referencia al registro de paciente en la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOCPACIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla cabecera HCENTREGATURNOCPACIENTE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOCPACIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOCPACIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cabecera turno general (tabla HCENTREGATURNOC). INT. Vincula referencia al turno o evento de atención donde se origina.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla cabecera HCENTREGATURNO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (PK) del detalle de referencia. INT IDENTITY(1,1). Clave primaria de la tabla de gestión de referencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autonumerico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de referencias o remisiones asociadas a la entrega de turno en historia clínica. Vincula cada paciente de un turno con su correspondiente referencia o interconsulta, controlando el estado del trámite mediante un folio de seguimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOREFEREN';
