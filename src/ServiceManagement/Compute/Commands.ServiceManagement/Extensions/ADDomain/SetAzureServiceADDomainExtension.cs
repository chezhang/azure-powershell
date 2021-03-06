﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Management.Automation;
using System.Security.Cryptography.X509Certificates;
using Microsoft.WindowsAzure.Commands.ServiceManagement.Model;
using Microsoft.WindowsAzure.Commands.Utilities.Common;

namespace Microsoft.WindowsAzure.Commands.ServiceManagement.Extensions
{
    /// <summary>
    /// Set Microsoft Azure Service AD Domain Extension.
    /// </summary>
    [Cmdlet(VerbsCommon.Set, ADDomainExtensionNoun, DefaultParameterSetName = DomainParameterSet), OutputType(typeof(ManagementOperationContext))]
    public class SetAzureServiceADDomainExtensionCommand : BaseAzureServiceADDomainExtensionCmdlet
    {
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true, HelpMessage = ExtensionParameterPropertyHelper.ServiceNameHelpMessage)]
        public override string ServiceName
        {
            get;
            set;
        }

        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true, HelpMessage = ExtensionParameterPropertyHelper.SlotHelpMessage)]
        [ValidateSet(DeploymentSlotType.Production, DeploymentSlotType.Staging, IgnoreCase = true)]
        public override string Slot
        {
            get;
            set;
        }

        [Parameter(Position = 2, ValueFromPipelineByPropertyName = true, HelpMessage = ExtensionParameterPropertyHelper.RoleHelpMessage)]
        [ValidateNotNullOrEmpty]
        public override string[] Role
        {
            get;
            set;
        }

        [Parameter(Position = 3, ValueFromPipelineByPropertyName = true, ParameterSetName = DomainParameterSet, HelpMessage = ExtensionParameterPropertyHelper.X509CertificateHelpMessage)]
        [Parameter(Position = 3, ValueFromPipelineByPropertyName = true, ParameterSetName = DomainJoinOptionParameterSet, HelpMessage = ExtensionParameterPropertyHelper.X509CertificateHelpMessage)]
        [Parameter(Position = 3, ValueFromPipelineByPropertyName = true, ParameterSetName = WorkgroupParameterSet, HelpMessage = ExtensionParameterPropertyHelper.X509CertificateHelpMessage)]
        [ValidateNotNullOrEmpty]
        public override X509Certificate2 X509Certificate
        {
            get;
            set;
        }

        [Parameter(Position = 3, ValueFromPipelineByPropertyName = true, Mandatory = true, ParameterSetName = DomainThumbprintParameterSet, HelpMessage = ExtensionParameterPropertyHelper.CertificateThumbprintHelpMessage)]
        [Parameter(Position = 3, ValueFromPipelineByPropertyName = true, Mandatory = true, ParameterSetName = DomainJoinOptionThumbprintParameterSet, HelpMessage = ExtensionParameterPropertyHelper.CertificateThumbprintHelpMessage)]
        [Parameter(Position = 3, ValueFromPipelineByPropertyName = true, Mandatory = true, ParameterSetName = WorkgroupThumbprintParameterSet, HelpMessage = ExtensionParameterPropertyHelper.CertificateThumbprintHelpMessage)]
        [ValidateNotNullOrEmpty]
        public override string CertificateThumbprint
        {
            get;
            set;
        }

        [Parameter(Position = 4, ValueFromPipelineByPropertyName = true, HelpMessage = ExtensionParameterPropertyHelper.ThumbprintAlgorithmHelpMessage)]
        [ValidateNotNullOrEmpty]
        public override string ThumbprintAlgorithm
        {
            get;
            set;
        }

        [Parameter(Position = 5, ValueFromPipelineByPropertyName = true, Mandatory = true, ParameterSetName = DomainParameterSet)]
        [Parameter(Position = 5, ValueFromPipelineByPropertyName = true, Mandatory = true, ParameterSetName = DomainThumbprintParameterSet)]
        [Parameter(Position = 5, ValueFromPipelineByPropertyName = true, Mandatory = true, ParameterSetName = DomainJoinOptionParameterSet)]
        [Parameter(Position = 5, ValueFromPipelineByPropertyName = true, Mandatory = true, ParameterSetName = DomainJoinOptionThumbprintParameterSet)]
        [ValidateNotNullOrEmpty]
        public override string DomainName
        {
            get
            {
                return base.DomainName;
            }
            set
            {
                base.DomainName = value;
            }
        }

        [Parameter(Position = 5, ValueFromPipelineByPropertyName = true, Mandatory = true, ParameterSetName = WorkgroupParameterSet)]
        [Parameter(Position = 5, ValueFromPipelineByPropertyName = true, Mandatory = true, ParameterSetName = WorkgroupThumbprintParameterSet)]
        [ValidateNotNullOrEmpty]
        public override string WorkgroupName
        {
            get
            {
                return base.WorkgroupName;
            }
            set
            {
                base.WorkgroupName = value;
            }
        }

        [Parameter(Position = 6, ValueFromPipelineByPropertyName = true)]
        public override SwitchParameter Restart
        {
            get
            {
                return base.Restart;
            }
            set
            {
                base.Restart = value;
            }
        }

        [Parameter(Position = 7, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public override PSCredential Credential
        {
            get
            {
                return base.Credential;
            }
            set
            {
                base.Credential = value;
            }
        }

        [Parameter(Position = 8, ValueFromPipelineByPropertyName = true, ParameterSetName = DomainParameterSet)]
        [Parameter(Position = 8, ValueFromPipelineByPropertyName = true, ParameterSetName = DomainThumbprintParameterSet)]
        [Parameter(Position = 8, ValueFromPipelineByPropertyName = true, ParameterSetName = DomainJoinOptionParameterSet)]
        [Parameter(Position = 8, ValueFromPipelineByPropertyName = true, ParameterSetName = DomainJoinOptionThumbprintParameterSet)]
        [ValidateNotNullOrEmpty]
        public override PSCredential UnjoinDomainCredential
        {
            get
            {
                return base.UnjoinDomainCredential;
            }
            set
            {
                base.UnjoinDomainCredential = value;
            }
        }

        [Parameter(Position = 9, ValueFromPipelineByPropertyName = true, ParameterSetName = DomainParameterSet)]
        [Parameter(Position = 9, ValueFromPipelineByPropertyName = true, ParameterSetName = DomainThumbprintParameterSet)]
        [ValidateNotNullOrEmpty]
        public override JoinOptions Options
        {
            get
            {
                return base.Options;
            }
            set
            {
                base.Options = value;
            }
        }

        [Parameter(Position = 10, Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = DomainJoinOptionParameterSet)]
        [Parameter(Position = 10, Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = DomainJoinOptionThumbprintParameterSet)]
        [ValidateNotNullOrEmpty]
        public override uint JoinOption
        {
            get
            {
                return base.JoinOption;
            }
            set
            {
                base.JoinOption = value;
            }
        }

        [Parameter(Position = 11, ValueFromPipelineByPropertyName = true, ParameterSetName = DomainParameterSet)]
        [Parameter(Position = 11, ValueFromPipelineByPropertyName = true, ParameterSetName = DomainThumbprintParameterSet)]
        [Parameter(Position = 11, ValueFromPipelineByPropertyName = true, ParameterSetName = DomainJoinOptionParameterSet)]
        [Parameter(Position = 11, ValueFromPipelineByPropertyName = true, ParameterSetName = DomainJoinOptionThumbprintParameterSet)]
        [ValidateNotNullOrEmpty]
        public override string OUPath
        {
            get
            {
                return base.OUPath;
            }
            set
            {
                base.OUPath = value;
            }
        }

        [Parameter(Position = 12, ValueFromPipelineByPropertyName = true, ParameterSetName = DomainParameterSet)]
        [Parameter(Position = 12, ValueFromPipelineByPropertyName = true, ParameterSetName = DomainThumbprintParameterSet)]
        [Parameter(Position = 12, ValueFromPipelineByPropertyName = true, ParameterSetName = DomainJoinOptionParameterSet)]
        [Parameter(Position = 12, ValueFromPipelineByPropertyName = true, ParameterSetName = DomainJoinOptionThumbprintParameterSet)]
        [Parameter(Position = 8, ValueFromPipelineByPropertyName = true, ParameterSetName = WorkgroupParameterSet)]
        [Parameter(Position = 8, ValueFromPipelineByPropertyName = true, ParameterSetName = WorkgroupThumbprintParameterSet)]
        [ValidateNotNullOrEmpty]
        public override string Version
        {
            get;
            set;
        }

        protected override void ValidateParameters()
        {
            base.ValidateParameters();
            ValidateService();
            ValidateDeployment();
            ValidateRoles();
            ValidateThumbprint(true);
            ValidateConfiguration();
        }

        public void ExecuteCommand()
        {
            this.ValidateParameters();
            ExtensionConfigurationInput context = new ExtensionConfigurationInput
            {
                ProviderNameSpace = ProviderNamespace,
                Type = ExtensionName,
                CertificateThumbprint = CertificateThumbprint,
                ThumbprintAlgorithm = ThumbprintAlgorithm,
                X509Certificate = X509Certificate,
                PublicConfiguration = PublicConfiguration,
                PrivateConfiguration = PrivateConfiguration,
                Roles = new ExtensionRoleList(Role != null && Role.Any() ? Role.Select(r => new ExtensionRole(r)) : Enumerable.Repeat(new ExtensionRole(), 1))
            };

            var extConfig = ExtensionManager.InstallExtension(context, Slot, Deployment, PeerDeployment);
            ChangeDeployment(extConfig);
        }

        protected override void OnProcessRecord()
        {
            ExecuteCommand();
        }
    }
}
