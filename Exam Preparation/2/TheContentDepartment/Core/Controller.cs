using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Core.Contracts;
using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Models.Resources;
using TheContentDepartment.Models.TeamMembers;
using TheContentDepartment.Repositories;
using TheContentDepartment.Repositories.Contracts;
using TheContentDepartment.Utilities.Messages;

namespace TheContentDepartment.Core
{
    public class Controller : IController
    {
        private readonly IRepository<IResource> resources;
        private readonly IRepository<ITeamMember> members;
        public Controller()
        {
            resources = new ResourceRepository();
            members = new MemberRepository();
        }
        public string ApproveResource(string resourceName, bool isApprovedByTeamLead)
        {
            IResource resource = resources.TakeOne(resourceName);

            if(!resource.IsTested)
            {
                return string.Format(OutputMessages.ResourceNotTested, resourceName);
            }

            ITeamMember teamLead = members.Models.Single(x => x is TeamLead);

            if(isApprovedByTeamLead)
            {
                resource.Approve();
                teamLead.FinishTask(resourceName);

                return string.Format(OutputMessages.ResourceApproved, teamLead.Name, resourceName);
            }

            resource.Test();

            return string.Format(OutputMessages.ResourceReturned, teamLead.Name, resourceName);
        }

        public string CreateResource(string resourceType, string resourceName, string path)
        {
            if((resourceType != nameof(Exam))
                && (resourceType != nameof(Workshop))
                && (resourceType != nameof(Presentation)))
            {
                return string.Format(OutputMessages.ResourceTypeInvalid, resourceType);
            }

            ITeamMember contentMember = members.Models.FirstOrDefault(x => x.Path == path);
            if(contentMember is null || contentMember is not ContentMember)
            {
                return string.Format(OutputMessages.NoContentMemberAvailable, resourceName);
            }
            if(contentMember.InProgress.Contains(resourceName))
            {
                return string.Format(OutputMessages.ResourceExists, resourceName);
            }

            IResource resource = null;
            if(resourceType == nameof(Exam))
            {
                resource = new Exam(resourceName, contentMember.Name);
            }
            else if(resourceType == nameof(Workshop))
            {
                resource = new Workshop(resourceName, contentMember.Name);
            }
            else if(resourceType == nameof(Presentation))
            {
                resource = new Presentation(resourceName, contentMember.Name);
            }

            contentMember.WorkOnTask(resourceName);
            resources.Add(resource);

            return string.Format(OutputMessages.ResourceCreatedSuccessfully, resource.Creator, resourceType, resourceName);
        }

        public string DepartmentReport()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Finished Tasks:");

            foreach(IResource resource in resources.Models.Where(x => x.IsApproved))
            {
                sb.AppendLine($"--{resource}");
            }

            sb.AppendLine("Team Report:");

            ITeamMember teamLead = members.Models.Single(x => x is TeamLead);

            sb.AppendLine($"--{teamLead}");

            foreach(ITeamMember member in members.Models)
            {
                if(member != teamLead)
                {
                    sb.AppendLine(member.ToString());
                }
            }

            return sb.ToString().TrimEnd();
        }

        public string JoinTeam(string memberType, string memberName, string path)
        {
            ITeamMember teamMember;
            switch(memberType)
            {
                case nameof(TeamLead):
                    teamMember = new TeamLead(memberName, path);
                    break;

                case nameof(ContentMember):
                    teamMember = new ContentMember(memberName, path);
                    break;

                default:
                    return string.Format(OutputMessages.MemberTypeInvalid, memberType);
            }

            if(teamMember is ContentMember && members.Models.Any(x => x.Path == path))
            {
                return OutputMessages.PositionOccupied;
            }
            if(MemberExists(memberName))
            {
                return string.Format(OutputMessages.MemberExists, memberName);
            }

            members.Add(teamMember);

            return string.Format(OutputMessages.MemberJoinedSuccessfully, memberName);
        }

        public string LogTesting(string memberName)
        {
            ITeamMember member = members.Models.FirstOrDefault(x => x.Name == memberName);
            if(member is null)
            {
                return OutputMessages.WrongMemberName;
            }

            IResource resource = resources.Models
                .Where(x => !x.IsTested && x.Creator == member.Name)
                .OrderBy(x => x.Priority)
                .FirstOrDefault();

            if(resource is null)
            {
                return string.Format(OutputMessages.NoResourcesForMember, memberName);
            }

            ITeamMember teamLead = members.Models.Single(x => x is TeamLead);

            member.FinishTask(resource.Name);
            teamLead.WorkOnTask(resource.Name);

            resource.Test();

            return string.Format(OutputMessages.ResourceTested, resource.Name);
        }
        private bool MemberExists(string name)
        {
            return members.TakeOne(name) is not null;
        }
    }
}
