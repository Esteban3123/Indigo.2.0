#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IAccountingMassiveReplication

#Region "Methods"

    ''' <summary>
    ''' Consume el sp de replicación masiva
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SP_MassiveReplication(InitialDate As Date?, EndDate As Date?, BookOriginId As Integer, BookDestinationId As Integer, CodeInitialJournalVoucherType As String, CodeEndJournalVoucherType As String) As ActionResult(Of SP_MassiveReplication_Result)

    ''' <summary>
    ''' Metodo para realizar replicacion masiva 2.0, la otra replicacion la realiza dentro de un ciclo.
    ''' </summary>
    <OperationContract()>
    Function SP_MassiveReplication2(InitialDate As Date?, EndDate As Date?, BookOriginId As Integer, BookDestinationId As Integer, session As SessionValues) As ActionResult


#End Region

End Interface
