CREATE TABLE [Contract].[SurgicalGroup] (
    [Id]                      INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                    VARCHAR (20)   NOT NULL,
    [Name]                    VARCHAR (100)  NOT NULL,
    [SurgeonService]          NUMERIC (5, 2) CONSTRAINT [DF_SurgicalGroup_SurgeonService] DEFAULT ((0)) NOT NULL,
    [AnesthesiologistService] NUMERIC (5, 2) CONSTRAINT [DF_SurgicalGroup_AnesthesiologistService] DEFAULT ((0)) NOT NULL,
    [AssistantService]        NUMERIC (5, 2) CONSTRAINT [DF_SurgicalGroup_AssistantService] DEFAULT ((0)) NOT NULL,
    [RoomService]             NUMERIC (5, 2) CONSTRAINT [DF_SurgicalGroup_RoomService] DEFAULT ((0)) NOT NULL,
    [MaterialsService]        NUMERIC (5, 2) CONSTRAINT [DF_SurgicalGroup_MaterialsService] DEFAULT ((0)) NOT NULL,
    [Status]                  BIT            NOT NULL,
    [CreationUser]            VARCHAR (20)   NOT NULL,
    [CreationDate]            DATETIME       NOT NULL,
    [ModificationUser]        VARCHAR (20)   NULL,
    [ModificationDate]        DATETIME       NULL,
    [TimeStamp]               ROWVERSION     NOT NULL,
    CONSTRAINT [PK_SurgicalGroup__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_SurgicalGroup__Code]
    ON [Contract].[SurgicalGroup]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal TIMESTAMP (tipo SQL Server) que registra automáticamente el instante exacto de creación, modificación o evento en el grupo quirúrgico. Control de auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME de la última modificación del grupo quirúrgico. Null si nunca fue editado tras creación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario VARCHAR(20) que realizó la última modificación del registro del grupo quirúrgico. Null si no ha habido cambios.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME de creación del grupo quirúrgico en el sistema. Registro inicial del grupo en contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario VARCHAR(20) que creó el registro del grupo quirúrgico. Auditoría de origen del dato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado BIT del grupo quirúrgico: 1=Activo (vigente en contrato y facturación), 0=Inactivo (descontinuado, no disponible para cirugías).', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del manual tarifario  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor SOAT NUMERIC(5,2) multiplicador del costo de materiales quirúrgicos por grupo. Porcentaje/valor para valorización de insumos en procedimientos.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'MaterialsService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Factor soat del servicio de materiales', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'MaterialsService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'MaterialsService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor SOAT NUMERIC(5,2) multiplicador del costo de sala de cirugía por grupo. Tarifa de ocupación de quirófano según grupo procedimiento.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'RoomService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Factor Soat del servicio de sala', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'RoomService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'RoomService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor SOAT NUMERIC(5,2) multiplicador del costo del servicio de ayudante quirúrgico. Honorario del cirujano asistente en grupo.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'AssistantService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Factor Soat del servicio de ayudante', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'AssistantService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'AssistantService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor SOAT NUMERIC(5,2) multiplicador del costo del servicio de anestesiólogo. Honorario anestesiología según grupo quirúrgico.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'AnesthesiologistService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Factor Soat dle servicio anestesiologo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'AnesthesiologistService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'AnesthesiologistService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor SOAT NUMERIC(5,2) multiplicador del costo del servicio de cirujano. Honorario principal del cirujano por grupo procedimiento.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'SurgeonService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Factor Soat del servicio cirujano', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'SurgeonService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'SurgeonService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre VARCHAR(100) descriptivo del grupo quirúrgico. Ej: ''''Cirugía General'''', ''''Ortopedia'''', ''''Oftalmología''''. Identificación legible.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'nombre del grupo quirurgico', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código VARCHAR(20) único del grupo quirúrgico. Identificador único del grupo en contrato y facturación. Clave de búsqueda.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del grupo quirurgico', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT IDENTITY único del grupo quirúrgico. Clave primaria autonumérica. Referencia en procedimientos y facturas.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo quirurgico', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupos quirúrgicos definidos en los contratos, con los porcentajes de participación o tarifas asignadas a cada rol del equipo quirúrgico (cirujano, anestesiólogo, ayudante, sala y materiales). Se usa para liquidar y distribuir el valor de los procedimientos quirúrgicos según el contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup';
