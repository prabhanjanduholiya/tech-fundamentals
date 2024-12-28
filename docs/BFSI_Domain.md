# BFSI

* ***What is SWIFT?***
SWIFT is world’s leading provider of secure financial messaging services. 
Solutions that SWIFT provides are for reliable and rapid exchange of financial messages.

* ***What are SWIFT Standards?***

## MT Standards:
An MT (message text) is a traditional message type for use on the SWIFT network. The message text standards have been developed to support the business transactions of SWIFT users. 

#### MT 103: Single Customer Credit Transfer
* It allows the exchange of single customer credit transfers using all MT 103 fields.
* This message type is sent by or on behalf of the financial institution of the ordering customer, directly or through (a) correspondent(s), to the financial institution of the beneficiary customer.

###### Field- 23E 
Mandatory 4 Digit instruction code. 

###### Field-50
This field specifies the customer ordering the transaction

###### Field-59
This field specifies the customer which will be paid.

###### Field-71
* 71A - This field specifies which party will bear the charges for the transaction.
* 71F - Sender's Charges
* 71G - Receiver's Charges

###### Field-72
This field specifies additional information for the Receiver or other party specified.

#### MT 202: General Financial Institution Transfer
* All parties to the transaction must be financial institutions.
* Requests the movement of funds between financial institutions.

https://www2.swift.com/knowledgecentre/products/Standards%20MT/publications#November%202021

## MX Standards: 
An MX is an XML message definition for use on the SWIFT network. Most MX messages are also ISO 20022 messages.

### PCAS: Payments Clearing and Settlement
#### pacs008: FIToFICustomerCreditTransferV10 [XSD can be downloaded]
The FinancialInstitutionToFinancialInstitutionCustomerCreditTransfer message is sent by the debtor
agent to the creditor agent, directly or through other agents and/or a payment clearing and settlement
system. It is used to move funds from a debtor account to a creditor.

* Cdtr-Party to which an amount of money is due.
* Dbtr-Party on the debit side of the transaction to which the tax applies.
* InstrnForNxtAgt
Further information related to the processing of the payment instruction that may need to be
acted upon by the next agent.
Usage: The next agent may not be the creditor agent.
The instruction can relate to a level of service, can be an instruction that has to be executed by the
agent, or can be information required by the next agent.

* InstrnForCdtrAgt
Further information related to the processing of the payment instruction, provided by the
initiating party, and intended for the creditor agent.

#### pacs009: FinancialInstitutionCreditTransferV10 [XSD can be downloaded]
The FinancialInstitutionCreditTransfer message is sent by a debtor financial institution to a creditor
financial institution, directly or through other agents and/or a payment clearing and settlement system.
It is used to move funds from a debtor account to a creditor, where both debtor and creditor are
financial institutions.

https://www.iso20022.org/iso-20022-message-definitions

**Query**: 
What is cover method?
