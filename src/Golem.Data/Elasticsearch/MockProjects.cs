using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Golem.Data.Elasticsearch.Models;
using Nest;

namespace Golem.Data.Elasticsearch
{
    public class MockProjects
    {
        private readonly ElasticClient client;
        private readonly ElasticsearchSettings settings;

        public MockProjects(ElasticClient client,
            ElasticsearchSettings settings)
        {
            this.client = client;
            this.settings = settings;
        }

        public async Task RunAsync()
        {
            //TODO: remove from production
            var index = await client.Indices.ExistsAsync(settings.Index);

            if (index.Exists)
            {
                await client.Indices.DeleteAsync(settings.Index);
            }

            var createResult =
                await client.Indices.CreateAsync(settings.Index, c => c
                    .Settings(s => s
                        .Analysis(a => a
                            .AddCustomSearchAnalyzer()
                        )
                    )
                    .Map<Project>(m => m.AutoMap())
                );


            var bulkResult =
                await client
                    .BulkAsync(b => b
                        .Index(settings.Index)
                        .CreateMany(CreateProjects())
                    );
        }


        private IEnumerable<Project> CreateProjects()
        {
            return new List<Project>
            {
                new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Success Coach",
                    Description =
                        "Welcome visitors to your site with a bright and inspiring template. You and your services are highlighted on the homepage, while Wix Bookings helps to organize your calendar and makes it easy for prospective clients to schedule a session. Click edit and take your first step towards online success."
                },
                new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Construction Company",
                    Description =
                        "Build a website as impressive as your constructions with this attractive and professional construction company template. With an area to promote your services and attractive portfolio pages, this is the perfect website template for anyone wishing to showcase their projects and attract clients. Simply click to begin editing and promote your business online today!"
                },
                new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Sports Nutrition Store",
                    Description =
                        "You need a website that will get you in stride with your site visitors, and this template sets the mark. Engage your online community with a Wix Blog, and makes it easy for customers to find the product they want with a Wix Store. Stay connected with your users when they sign up for updates and set your business up for success."
                },
                new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "The Consultant",
                    Description =
                        "Give your consulting firm an edge with this sharp website template. Customize the text to describe your services and add images to express the energy and drive of your business. Start editing to build your online presence!"
                },
                new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Modeling Agency",
                    Description =
                        "A chic, classic template that is all about featuring that fresh new face. Simply build look books filled with eye-catching, high quality images, and add useful information such as how to book your talent or apply to join the agency. You can also use Wix Chat to easily communicate with potential clients."
                },
                new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Virtual Assistant",
                    Description =
                        "You're a self-made professional with a knack for organization and management. Attract clients and boost your online business by customizing this efficient and engaging template design. Connect with site visitors and earn their trust by highlighting your services and rates, sharing client testimonials, and being proactive with Wix Chat. The road to a successful online business starts right here!"
                },
                new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Food Photographer",
                    Description =
                        "Showcase the beauty and diversity of cuisine with this elegant and attractive food photography template. Fill your portfolio with multiple image galleries, engaging blog posts, and featured photo collections to keep everyone updated on your latest and greatest work. Click edit and share your eye for delicious design."
                },
                new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Graffiti Artist",
                    Description =
                        "Your artwork and creative projects are displayed front-and-center with this visually appealing template. Striking black and white media along with parallax scrolling provide the perfect backdrop to showcase your colorful and unique style. Click edit and start sharing your artistic vision."
                },
                new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Plant Boutique",
                    Description =
                        "Plants provide a variety of fresh and vibrant feels that complement any space. What better way to display your store's diversity of greenery than with this stunning boutique plant shop template. Let customers browse through your botanical collections while experiencing the ease and convenience of shopping online."
                },
                new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Architect Company",
                    Description =
                        "Show off your business' style with this sleek and chic architectural website template. With stunning images and a design that makes your projects pop, your viewers will not be able to get enough of all that you have to offer. Simply click 'Edit' to start crafting your own website today."
                },
                new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Personal Blog",
                    Description =
                        "It’s time to bring your point of view to life on the screen. This clean, minimal template is the perfect platform to promote yourself or your business, grow your following, or even make some money. Get started with the Wix Blog to share your experiences, adventures, or daily life."
                },
                new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Gaming Forum",
                    Description =
                        "Every serious gamer deserves a seriously immersive forum, and this template has everything you need to get started. Wix Forum is the perfect space for your site members to share interesting posts and discuss the latest gaming news, while Wix Events lets you organize upcoming meetups and manage live events like a pro. Don't be a noob, create your forum site today!"
                },
                new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Moving and Storage Service",
                    Description =
                        "Finding a dependable and experienced professional is stressful. You know that better than anyone. That's why you offer your friendly, trustworthy and reliable services to local residents who need you to make the process run smoothly and efficiently. This template focuses on the vital information about your business, with Wix Forms & Wix Chat ready to help personalize your interaction with site visitors and secure new clients."
                },
                new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Travel Forum",
                    Description =
                        "There's no better way to learn about the topics you're interested in than from a community of real people with real experiences. This template makes a great starting point for creating a fun and friendly interactive online community. Easily setup and manage your Wix Forum where your members can share tips, swap stories, ask questions and start discussions."
                },
                new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Dietitian",
                    Description =
                        "Showcase your professional qualifications with this fresh and eye-catching dietitian template. With a fully integrated booking engine to create a variety of different appointments, this template is perfect for nutritionists, dieticians, and other health consultants. Use the Blog page to keep your followers up to date on your latest activities. Simply start editing now to create your online presence and watch your clientele grows!"
                },
                new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Advertising and Marketing Firm",
                    Description =
                        "The minimal use of color and sleek design give this template a cool and modern vibe. Promote your work in the collage-style gallery and add text to describe your services. Create a website worthy of your creative vision."
                },
                new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Ballet School",
                    Description =
                        "A template that moves with as much grace and elegance as your students. Wix Bookings makes it easy for students to enroll in classes, while the Wix Blog and Events Widget are great ways to keep site visitors informed with the latest announcements, updates and details of your special events. Simply click ‘Edit’ to get started."
                },
                new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Mental Health Blog",
                    Description =
                        "Inspire brighter days ahead for you and your readers with this personal blog. This friendly, upbeat template is a great way to advocate your cause and watch your subscribers grow. Share intimate stories about your journey, advice and helpful resources that will give your readers the comfort they seek. Just click ‘Edit’ to begin - it’s all it takes."
                },
                new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Concert Venue",
                    Description =
                        "Bold and dynamic, the hip-hop nature of this rap-friendly venue template puts the spotlight on your unique concert scene and everything you have to offer. The highly visual design showcases your videos and photos, and visitors can easily purchase show tickets through Wix Events. Simply click edit to begin."
                },
                new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Business Services",
                    Description =
                        "A classic and sleek website template awaits your consulting, financial, or corporate company. Describe your services and qualifications and customize the design to suit your taste. Start editing to take your firm to the next level!"
                }
            };
        }
    }
}
