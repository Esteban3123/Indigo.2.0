CREATE TABLE [Authorization].[AuthorizationOutsourcedServices] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                  VARCHAR (20)  NOT NULL,
    [DocumentDate]          DATETIME      NOT NULL,
    [Type]                  TINYINT       NOT NULL,
    [AdmissionNumber]       VARCHAR (10)  NULL,
    [ThirdPartyId]          INT           NULL,
    [Description]           VARCHAR (MAX) NULL,
    [OperatingUnitId]       INT           NOT NULL,
    [Status]                TINYINT       NOT NULL,
    [StatusInAuthorization] TINYINT       CONSTRAINT [DF_AuthorizationOutsourcedServices_StatusInAuthorization] DEFAULT ((0)) NOT NULL,
    [StatusInBilling]       TINYINT       CONSTRAINT [DF_AuthorizationOutsourcedServices_StatusInBilling] DEFAULT ((0)) NOT NULL,
    [CreationUser]          VARCHAR (20)  CONSTRAINT [DF_AuthorizationOutsourcedServices_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]          DATETIME      CONSTRAINT [DF_AuthorizationOutsourcedServices_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]      VARCHAR (20)  NULL,
    [ModificationDate]      DATETIME      NULL,
    [ConfirmationUser]      VARCHAR (20)  NULL,
    [ConfirmationDate]      DATETIME      NULL,
    [AnnulmentUser]         VARCHAR (20)  NULL,
    [AnnulmentDate]         DATETIME      NULL,
    [TimeStamp]             ROWVERSION    NOT NULL,
    CONSTRAINT [PK_AuthorizationOutsourcedServices] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationOutsourcedServices_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServices_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de Tiempo.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServices', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado en facturación:    1. Realizado: Cuando dentro flujo de proceso de facturación se asocie a una orden de servicios en estado “Registrado”    2. Facturado: Cuando dentro flujo de proceso de facturación se asocie a una orden de servicios en estado “Facturado”', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServices', @level2type = N'COLUMN', @level2name = N'StatusInBilling';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado en autorizaciones:    1. Autorizado: Cuando dentro del flujo de proceso de autorización se asocie a una orden médica en estado “Autorizado”    2. No Autorizado: Cuando dentro flujo de proceso de autorización se asocie a una orden médica en estado “No Autorizado”    3. En trámite: Cuando dentro flujo de proceso de autorización se asocie a una orden médica en estado “Tramitado”', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServices', @level2type = N'COLUMN', @level2name = N'StatusInAuthorization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Documento   Registrado = 1  Confirmado = 2  Anulado = 3', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServices', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de cotización:  1- Intrahospitalario  2- Ambulatoria', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServices', @level2type = N'COLUMN', @level2name = N'Type';

