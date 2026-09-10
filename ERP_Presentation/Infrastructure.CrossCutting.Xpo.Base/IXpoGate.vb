' ***********************************************************************
' Assembly         : Infrastructure.CrossCutting.Xpo.Base
' Author           : Oscar Sierra
' Created          : 2011-08-03
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-25
' 
' Copyright        : (c) . All rights reserved.
' ***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo.DB
Imports System.ServiceModel
Imports DevExpress.Xpo.DB.Helpers
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpo.DB.Exceptions
Imports DevExpress.Xpo.Metadata
#End Region

''' <summary>
''' Contrato Xpo
''' </summary>
<ServiceContractAttribute(Namespace:="http://www.genesis.com.co", ConfigurationName:="IXpoGate")> _
Public Interface IXpoGate
    ''' <summary>
    ''' Modifies the data.	
    ''' </summary>
    ''' <param name="INDEmpresa">Empresa.</param>
    ''' <param name="dmlStatements">The DML statements.</param>
    ''' <returns></returns>
    <ServiceKnownType(GetType(DeleteStatement)),
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
    ServiceKnownType(GetType(ConstantValue)),
    OperationContract(Action:="http://www.genesis.com.co/IXpoGate/ModifyData", ReplyAction:="http://www.genesis.com.co/IXpoGate/ModifyDataResponse"),
    FaultContract(GetType(LockingException), Action:="http://www.genesis.com.co/IXpoGate/ModifyDataLockingExceptionFault", Name:="LockingException", Namespace:="http://schemas.datacontract.org/2004/07/DevExpress.Xpo.DB.Exceptions")> _
    Function ModifyData(ByVal company As String, ByVal ParamArray dmlStatements As ModificationStatement()) As ModificationResult

    ''' <summary>
    ''' UpdateSchema.	
    ''' </summary>
    ''' <param name="dontCreateIfFirstTableNotExist">dontCreateIfFirstTableNotExist.</param>
    ''' <param name="tables">tables.</param>
    ''' <returns></returns>

    <ServiceKnownType(GetType(DBColumn))> _
    <ServiceKnownType(GetType(DBIndex))> _
    <ServiceKnownType(GetType(DBPrimaryKey))> _
    <OperationContract(Action:="http://www.genesis.com.co/IXpoGate/UpdateSchema", ReplyAction:="http://www.genesis.com.co/IXpoGate/UpdateSchemaResponse")> _
    Function UpdateSchema(ByVal dontCreateIfFirstTableNotExist As Boolean, ByVal ParamArray tables As DBTable()) As UpdateSchemaResult

    ''' <summary>
    ''' GetAutoCreateOption.	
    ''' </summary>
    ''' <returns></returns>
    <OperationContract(Action:="http://www.genesis.com.co/IXpoGate/GetAutoCreateOption", ReplyAction:="http://www.genesis.com.co/IXpoGate/GetAutoCreateOption")> _
    Function GetAutoCreateOption() As AutoCreateOption


    ''' <summary>
    ''' Selects the data.	
    ''' </summary>
    ''' <param name="optionDB">Opción de Base de Datos.</param>
    ''' <param name="company">The company.</param>
    ''' <param name="selects">The selects.</param>
    ''' <returns></returns>
    <ServiceKnownType(GetType(AggregateOperand)),
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
    ServiceKnownType(GetType(ConstantValue)),
    OperationContract(Action:="http://www.genesis.com.co/IXpoGate/SelectData", ReplyAction:="http://www.genesis.com.co/IXpoGate/SelectDataResponse")> _
    Function SelectData(ByVal company As String, ByVal ParamArray selects As SelectStatement()) As SelectedData


End Interface
