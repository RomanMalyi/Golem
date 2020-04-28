using System;
using System.Collections.Generic;

namespace Golem.Data.Elasticsearch
{
    public class MlProject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; } //not for search!!!
        public int Price { get; set; }
        public Currency Currency { get; set; }
        public PaymentMethod PaymentMethod { get; set; } //Subscription,OneTimePurchase
        public bool AgeRestriction { get; set; }
        public List<Language> Languages { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset LastUpdate { get; set; }

        public long NumberOfTimesInSearch { get; set; }
        public long Views { get; set; }
        public long DuplicateViews { get; set; } //more than one view from one person
        public int NumberOfPurchases { get; set; }
        public int CommentsCount { get; set; } //do we need this?

        public ProgressStatus ProgressStatus { get; set; } //AlmostReady,Medium,Low
        public Producer Producer { get; set; }             //Own,Indirect
        public string CompanyName { get; set; }
        public byte CompanyRating { get; set; } // 0-100 percentage? our own rating

        public List<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public List<SoftwareFramework> SoftwareFrameworks { get; set; }
        public List<MarkupLanguage> MarkupLanguages { get; set; }
        public StyleSheetLanguage? StyleSheetLanguage { get; set; } //Css,Scss,Sass,Less

        public RealizationPart RealizationPart { get; set; } //FrontEnd,BackEnd,Both ***
        public ArchitecturalStructure ArchitecturalStructure { get; set; }
        public bool? AdaptiveWebDesign { get; set; }
        public CodebaseSize CodebaseSize { get; set; } //Big,Medium,Small
        public List<Documentation> Documentation { get; set; }
        public bool DevelopersSupport { get; set; }

        public string DocumentType { get; set; } //internal DB stuff
    }

    public enum Category
    {
        ArtAndDesign,
        AutoAndVehicles,
        Beauty,
        BooksAndReference,
        Business,
        Comics,
        Communication,
        Dating,
        Education,
        Entertainment,
        Events,
        Family,
        Finance,
        FoodAndDrink,
        Games,
        HealthAndFitness,
        HouseAndHome,
        LibrariesAndDemo,
        Lifestyle,
        MapsAndNavigation,
        Medical,
        MusicAndAudio,
        NewsAndMagazines,
        Parenting,
        Personalization,
        Photography,
        Productivity,
        Shopping,
        Social,
        Sports,
        Tools,
        TravelAndLocal,
        VideoPlayersAndEditors,
        Weather,
        Other//not recommended
    }

    public enum ArchitecturalStructure
    {
        ComponentBased,
        MonolithicApplication,
        Layered,
        PipesAndFilters,
        Microservices
    }

    public enum Documentation
    {
        None,
        UserManuals,
        ProjectDocumentation,
        RequirementsDocumentation,
        ArchitectureDocumentation,
        TechnicalDocumentation
    }

    public enum PaymentMethod
    {
        Subscription,
        OneTimePurchase
    }

    public enum Currency
    {
        UnitedStatesDollar,
        Euro
    }

    public enum CodebaseSize
    {
        Big,
        Medium,
        Small
    }

    public enum RealizationPart
    {
        FrontEnd,
        BackEnd,
        Both
    }

    public enum Language
    {
        English,
        French,
        German
    }

    public enum ProgrammingLanguage
    {
        CSharp,
        Python,
        Java,
        JavaScript,
        TypeScript,
        Php,
        Ruby
    }

    public enum SoftwareFramework
    {
        AspNet,
        AspNetCore,
        RubyOnRails,
        Django,
        Angular,
        Meteor,
        Laravel,
        Express,
        Spring,
        Bootstrap
    }

    public enum MarkupLanguage
    {
        Xml,
        Html,
        Xhtml
    }

    public enum StyleSheetLanguage
    {
        Css,
        Scss,
        Sass,
        Less
    }

    public enum ProgressStatus
    {
        AlmostReady,
        Medium,
        Low
    }

    public enum Producer
    {
        Own,
        Indirect
    }
}
