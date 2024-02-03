export interface IMenuItemModel {
    label: string;
    href: string | Array<string | number>;
    iconName?: string;
    additionalRoute?: string;
    children?: Array<IMenuItemModel>;
}
