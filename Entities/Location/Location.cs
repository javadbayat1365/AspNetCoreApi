using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Location
{
    public class Location:BaseEntity
    {
        public string Name { get; set; }
        public long BaseInfo_LocationType_ID { get; set; }
        public long? ParentLocation_ID { get; set; }

        [ForeignKey(nameof(ParentLocation_ID))]
        public Location Parentlocation { get; set; }
        public ICollection<Location> ChildLocations { get; set; }
        [ForeignKey(nameof(BaseInfo_LocationType_ID))]
        public BaseInfo baseInfo_LocationType { get; set; }
    }
}
