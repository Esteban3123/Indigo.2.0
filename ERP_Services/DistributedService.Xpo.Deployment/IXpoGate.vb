'***********************************************************************
' Assembly         : DistributedService.Xpo.Deployment
' Author           : WalterSierra
' Created          : 11-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-03-05
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Xpo.DB
Imports System.ServiceModel
Imports DevExpress.Xpo.DB.Helpers
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpo.DB.Exceptions
Imports DevExpress.Xpo.Metadata
#End Region

''' <summary>
''' Interface con los metodos necesarios para el manejo de entidades XPO, a traves de WCF
''' </summary>
<ServiceContract(Namespace:="http://www.genesis.com.co")> _
Public Interface IXpoGate

    <OperationContract(),
    FaultContract(GetType(LockingException)),
    ServiceKnownType(GetType(DeleteStatement)),
    ServiceKnownType(GetType(InsertStatement)),
    ServiceKnownType(GetType(UpdateStatement)),
    ServiceKnownType(GetType(AggregateOperand)),
    ServiceKnownType(GetType(BetweenOperator)),
    ServiceKnownType(GetType(BinaryOperator)),
    ServiceKnownType(GetType(ContainsOperator)),
    ServiceKnownType(GetType(FunctionOperator)),
    ServiceKnownType(GetType(GroupOperator)),
    ServiceKnownType(GetType(InOperator)),
    ServiceKnownType(GetType(NotOperator)),
    ServiceKnownType(GetType(NullOperator)),
    ServiceKnownType(GetType(OperandProperty)),
    ServiceKnownType(GetType(OperandValue)),
    ServiceKnownType(GetType(ParameterValue)),
    ServiceKnownType(GetType(QueryOperand)),
    ServiceKnownType(GetType(UnaryOperator)),
    ServiceKnownType(GetType(JoinOperand)),
    ServiceKnownType(GetType(OperandParameter)),
    ServiceKnownType(GetType(QuerySubQueryContainer)),
    ServiceKnownType(GetType(ConstantValue))> _
    Function ModifyData(ByVal company As String, ByVal ParamArray dmlStatements As ModificationStatement()) As ModificationResult

    <OperationContract()> _
    <ServiceKnownType(GetType(DBColumn))> _
    <ServiceKnownType(GetType(DBIndex))> _
    <ServiceKnownType(GetType(DBPrimaryKey))> _
    Function UpdateSchema(ByVal dontCreateIfFirstTableNotExist As Boolean, ByVal ParamArray tables As DBTable()) As UpdateSchemaResult

    <OperationContract> _
    Function GetAutoCreateOption() As AutoCreateOption

    ''' <summary>
    ''' Selects the data.	
    ''' </summary>
    ''' <param name="company">The company.</param>
    ''' <param name="selects">The selects.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract(),
    ServiceKnownType(GetType(AggregateOperand)),
    ServiceKnownType(GetType(BetweenOperator)),
    ServiceKnownType(GetType(BinaryOperator)),
    ServiceKnownType(GetType(ContainsOperator)),
    ServiceKnownType(GetType(FunctionOperator)),
    ServiceKnownType(GetType(GroupOperator)),
    ServiceKnownType(GetType(InOperator)),
    ServiceKnownType(GetType(NotOperator)),
    ServiceKnownType(GetType(NullOperator)),
    ServiceKnownType(GetType(OperandProperty)),
    ServiceKnownType(GetType(OperandValue)),
    ServiceKnownType(GetType(ParameterValue)),
    ServiceKnownType(GetType(QueryOperand)),
    ServiceKnownType(GetType(UnaryOperator)),
    ServiceKnownType(GetType(JoinOperand)),
    ServiceKnownType(GetType(OperandParameter)),
    ServiceKnownType(GetType(QuerySubQueryContainer)),
    ServiceKnownType(GetType(ConstantValue))> _
    Function SelectData(ByVal company As String, ByVal ParamArray selects As SelectStatement()) As SelectedData

End Interface
